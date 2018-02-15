using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Hosting;

namespace MovieCatalogService
{
    public class Service : IService
    {
        #region constructor & properties
        private string FilePath { get; set; }

        public Service()
        {
            FilePath = HostingEnvironment.MapPath(@"~\Data\MovieCatalog.json");
        }
        #endregion

        #region public methods
        public string GetMovies()
        {
            string data = string.Empty;
            WebOperationContext operationContext = WebOperationContext.Current;
            try
            {
                data = File.ReadAllText(FilePath);
            }
            catch (Exception ex)
            {
                data = ex.Message;
                operationContext.OutgoingResponse.StatusCode =
                    System.Net.HttpStatusCode.InternalServerError;
            }
            return data;
        }

        public string UpdateMovie()
        {
            IEnumerable<IFilm> movies;
            IFilm newMovie = new Movie();
            IFilm oldMovie = new Movie();
            WebOperationContext operationContext = WebOperationContext.Current;
            string outcome = string.Empty;
            try
            {
                newMovie = GetMovieFromRequestMessage();

                movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath));

                oldMovie = movies.Single(m => m.Id == newMovie.Id);
                oldMovie.Director = (oldMovie.Director != newMovie.Director) ?
                    newMovie.Director : oldMovie.Director;
                oldMovie.RunningTime = (oldMovie.RunningTime != newMovie.RunningTime) ?
                    newMovie.RunningTime : oldMovie.RunningTime;
                oldMovie.Title = (oldMovie.Title != newMovie.Title) ?
                    newMovie.Title : oldMovie.Title;
                oldMovie.Year = (oldMovie.Year != newMovie.Year) ?
                    newMovie.Year : oldMovie.Year;

                File.WriteAllText(FilePath, JsonConvert.SerializeObject(movies));
                outcome = newMovie.ToString();

            }
            catch (Exception ex)
            {
                outcome = ex.Message;
                operationContext.OutgoingResponse.StatusCode =
                    System.Net.HttpStatusCode.InternalServerError;
            }
            return outcome;
        }

        public void GetOptions() { }
        #endregion

        #region private methods
        // TO-DO: Replace this by using actual JSON-to-object parsing/binding either via 
        // Microsoft libraries or Newtonsoft.Json
        private IFilm GetMovieFromRequestMessage()
        {
            IFilm movie = new Movie();
            string requestMessage = OperationContext.Current.RequestContext.RequestMessage.ToString();

            movie.Id = new Guid(
                    FindInRequestMessage(requestMessage, "<id", 18, "id>", -20)
                    );
            movie.Title = FindInRequestMessage(
                requestMessage, "<title", 21, "title>", -23
                );
            movie.Director = FindInRequestMessage(
                requestMessage, "<director", 24, "director>", -26
                );
            movie.Year = int.Parse(
                FindInRequestMessage(requestMessage, "<year", 20, "year>", -22)
                );
            movie.RunningTime = int.Parse(
                FindInRequestMessage(requestMessage, "<runningTime", 27, "runningTime>", -29)
                );

            return movie;
        }

        // TO-DO: Replace this by using actual JSON-to-object parsing/binding either via 
        // Microsoft libraries or Newtonsoft.Json
        private string FindInRequestMessage(string requestMessage, string startSubString,
            int startOffset, string endSubString, int endOffset)
        {
            return requestMessage.Substring(requestMessage.IndexOf(startSubString) + startOffset,
                requestMessage.IndexOf(endSubString) - requestMessage.IndexOf(startSubString) + endOffset);
        }
        #endregion
    }
}

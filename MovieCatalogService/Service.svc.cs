using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    // TO-DO: Expose service to front end on localhost via config or other appropriate method
    public class Service : IService
    {
        private string FilePath { get; set; }

        public Service()
        {
            FilePath = @"~\Data\MovieCatalog.json";
        }
        public IEnumerable<Movie> GetMovies()
        {
            IEnumerable<Movie> movies;
            try
            {
                movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return movies;
        }

        public string UpdateMovie(string movieToUpdate)
        {
            string outcome = "failure";
            List<Movie> movies;
            Movie newMovie = JsonConvert.DeserializeObject<Movie>(movieToUpdate);
            Movie oldMovie;
            try
            {
                movies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath))).ToList<Movie>();
                oldMovie = movies.Single(m => m.Id == newMovie.Id);
                movies.Add(newMovie);
                movies.Remove(oldMovie);
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(movies));

                outcome = "success";
            }
            catch (Exception ex)
            {
                outcome += " " + ex.Message;
            }
            return outcome;
        }
    }
}

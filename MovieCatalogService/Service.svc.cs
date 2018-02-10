using System;
using System.Collections.Generic;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public List<Movie> GetMovies()
        {
            throw new NotImplementedException();
            List<Movie> movies = new List<Movie>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return movies;
        }

        public string UpdateMovie(Movie movieToUpdate)
        {
            throw new NotImplementedException();
            string outcome = "failure";
            try
            {
                outcome = "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outcome;
        }
    }
}

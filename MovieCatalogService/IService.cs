using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Movie",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Movie> GetMovies();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Movie/{Id}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateMovie(Movie movieToUpdate);
    }
}

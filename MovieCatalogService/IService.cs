using System.ServiceModel;
using System.ServiceModel.Web;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movie",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetMovies();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movie/{movieToUpdate}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateMovie(string movieToUpdate);
    }
}

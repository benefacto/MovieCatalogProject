using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web.Hosting;

namespace MovieCatalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    // TO-DO: Enable CORS so front end can access as per https://enable-cors.org/server_wcf.html
    public class Service : IService
    {
        private string FilePath { get; set; }

        public Service()
        {
            FilePath = HostingEnvironment.MapPath(@"~\Data\MovieCatalog.json");
        }
        public string GetMovies()
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
            return JsonConvert.SerializeObject(movies);
        }

        public string UpdateMovie(String[] movieJson)
        {
            string outcome = "failure";
            List<Movie> movies;
            Movie oldMovie;
            try
            {
                /*
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(movieJson[0]);
                movies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath))).ToList<Movie>();
                oldMovie = movies.Single(m => m.Id == new Guid(dict["id"].ToString()));
                oldMovie.Director = dict["director"].ToString();
                oldMovie.RunningTime = int.Parse(dict["runningTime"].ToString());
                oldMovie.Title = dict["title"].ToString();
                oldMovie.Year = int.Parse(dict["year"].ToString());
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(movies));
                
                outcome = "success" + " " + movieJson[0];
                */
            }
            catch (Exception ex)
            {
                outcome += " " + ex.ToString() + " " + movieJson[0];
            }
            return outcome;
        }

        public void GetOptions() { }
    }

    public class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        Dictionary<string, string> requiredHeaders;
        public CustomHeaderMessageInspector(Dictionary<string, string> headers)
        {
            requiredHeaders = headers ?? new Dictionary<string, string>();
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
            foreach (var item in requiredHeaders)
            {
                httpHeader.Headers.Add(item.Key, item.Value);
            }
        }
    }

    public class EnableCrossOriginResourceSharingBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            var requiredHeaders = new Dictionary<string, string>
            {
                { "Access-Control-Allow-Origin", "*" },
                { "Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS" },
                { "Access-Control-Allow-Headers", "X-Requested-With,Content-Type" },
                { "Access-Control-Allow-Methods", "PUT" }
            };

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomHeaderMessageInspector(requiredHeaders));
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        public override Type BehaviorType
        {
            get { return typeof(EnableCrossOriginResourceSharingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new EnableCrossOriginResourceSharingBehavior();
        }
    }
}

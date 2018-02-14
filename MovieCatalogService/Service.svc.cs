using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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

        public string UpdateMovie()
        {
            string outcome = "failure";
            List<Movie> movies;
            Movie newMovie = new Movie();
            Movie oldMovie = new Movie();
            String rawMessage;
            try
            {
                rawMessage = OperationContext.Current.RequestContext.RequestMessage.ToString();
                newMovie.Id = new Guid(rawMessage.Substring(rawMessage.IndexOf("<id") + 18, rawMessage.IndexOf("id>") - rawMessage.IndexOf("<id") - 20));
                newMovie.Title = rawMessage.Substring(rawMessage.IndexOf("<title") + 21, rawMessage.IndexOf("title>") - rawMessage.IndexOf("<title") - 23);
                newMovie.Director = rawMessage.Substring(rawMessage.IndexOf("<director") + 24, rawMessage.IndexOf("director>") - rawMessage.IndexOf("<director") - 26);
                newMovie.Year = int.Parse(rawMessage.Substring(rawMessage.IndexOf("<year") + 20, rawMessage.IndexOf("year>") - rawMessage.IndexOf("<year") - 22));
                newMovie.RunningTime = int.Parse(rawMessage.Substring(rawMessage.IndexOf("<runningTime") + 27, rawMessage.IndexOf("runningTime>") - rawMessage.IndexOf("<runningTime") - 29));

                movies = (JsonConvert.DeserializeObject<IEnumerable<Movie>>
                    (File.ReadAllText(FilePath))).ToList<Movie>();
                oldMovie = movies.Single(m => m.Id == newMovie.Id);
                oldMovie.Director = (oldMovie.Director != newMovie.Director) ? newMovie.Director : oldMovie.Director;
                oldMovie.RunningTime = (oldMovie.RunningTime != newMovie.RunningTime) ? newMovie.RunningTime : oldMovie.RunningTime;
                oldMovie.Title = (oldMovie.Title != newMovie.Title) ? newMovie.Title : oldMovie.Title;
                oldMovie.Year = (oldMovie.Year != newMovie.Year) ? newMovie.Year : oldMovie.Year;
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(movies));
                outcome = "success" + " " + newMovie.ToString();

            }
            catch (Exception ex)
            {
                outcome += " " + ex.ToString() + " " + newMovie.ToString();
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

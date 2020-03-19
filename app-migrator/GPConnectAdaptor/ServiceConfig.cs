using System;

namespace GPConnectAdaptor
{
    public interface IServiceConfig
    {
        string GetTargetDomain();
        string GetSourceDomain();
    }
    
    public class ServiceConfig : IServiceConfig
    {

        public string GetTargetDomain()
        {
            // docker
            return "http://" + Environment.GetEnvironmentVariable("demonstrator2") + ":" +
                   Environment.GetEnvironmentVariable("demonstratorport2") + "/gpconnect-demonstrator/v1/fhir";

            // local
            // return "http://localhost:19191/gpconnect-demonstrator/v1/fhir";
        }

        public string GetSourceDomain()
        {
            // docker
            return "http://" + Environment.GetEnvironmentVariable("demonstrator1") + ":" +
                   Environment.GetEnvironmentVariable("demonstratorport1")+"/gpconnect-demonstrator/v1/fhir";
            
            // local
            //return "http://localhost:19192/gpconnect-demonstrator/v1/fhir";
        }
    }
}
using System;

namespace GPConnectAdaptor
{
    public static class ServiceConfig
    {

        public static string GetTargetDomain()
        {
            return "http://localhost:19192/gpconnect-demonstrator/v1/fhir";
            // return "http://" + Environment.GetEnvironmentVariable("demonstrator2") + ":" +
            //        Environment.GetEnvironmentVariable("demonstratorport2") + "/gpconnect-demonstrator/v1/fhir";
        }

        public static string GetSourceDomain()
        {
            return "http://localhost:19191/gpconnect-demonstrator/v1/fhir";
            // return "http://" + Environment.GetEnvironmentVariable("demonstrator1") + ":" +
            //        Environment.GetEnvironmentVariable("demonstratorport1")+"/gpconnect-demonstrator/v1/fhir";
        }
    }
}
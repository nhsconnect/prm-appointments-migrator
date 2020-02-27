using System;

namespace GPConnectAdaptor
{
    public static class ServiceConfig
    {

        public static string GetTargetDomain()
        {
            return "http://" + Environment.GetEnvironmentVariable("demonstrator2") + ":" +
                   Environment.GetEnvironmentVariable("demonstratorport") + "/gpconnect-demonstrator/v1/fhir";
        }

        public static string GetSourceDomain()
        {
            return "http://" + Environment.GetEnvironmentVariable("demonstrator1") + ":" +
                   Environment.GetEnvironmentVariable("demonstratorport")+"/gpconnect-demonstrator/v1/fhir";
        }
    }
}
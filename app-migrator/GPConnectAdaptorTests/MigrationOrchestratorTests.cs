using System.IO;
using System.Reflection;
using GPConnectAdaptor;

namespace GPConnectAdaptorTests
{
    public class MigrationOrchestratorTests
    {
        private readonly string _file;
        
        public MigrationOrchestratorTests()
        {
            
            var assembly = typeof(MigrationOrchestratorTests).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream("GPConnectAdaptorTests.TestData.TestSlotResponse.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    _file = reader.ReadToEnd();
                }
            }
        }
    }
}
using ClassLibraryMocking1;
using ClassLibraryMocking1.Interfaces;
namespace UnitTestProjectMocking1
{
    internal class MockSystemConfig : IConfigureSystem
    {
        public bool LogStackForWasCalled { get; private set; }

        public bool LogStackFor(LogLevel logLevel)
        {
            LogStackForWasCalled = true;
            return true;
        }
    }
}
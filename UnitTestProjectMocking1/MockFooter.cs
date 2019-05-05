using ClassLibraryMocking1;
using ClassLibraryMocking1.Interfaces;
namespace UnitTestProjectMocking1
{
    internal class MockFooter : ICreateLogEntryFooter
    {
        public bool ForWasCalled { get; private set; }
        public void For(LogLevel logLevel)
        {
            ForWasCalled = true;
        }
    }
}
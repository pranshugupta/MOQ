using ClassLibraryMocking1;
using ClassLibraryMocking1.Interfaces;

namespace UnitTestProjectMocking1
{
    internal class MockHeader : ICreateLogEntryHeaders
    {
        public bool ForWasCalled { get; private set; }
        public void For(LogLevel logLevel)
        {
            ForWasCalled = true;
        }
    }
}
using ClassLibraryMocking1.Interfaces;

namespace UnitTestProjectMocking1
{
    internal class MockSrcubber : ISrcubSensitiveData
    {
        public bool FromWasCalled { get; private set; }
        public string From(string message)
        {
            FromWasCalled = true;
            return string.Empty;
        }
    }
}
using ClassLibraryMocking1;
using NUnit.Framework;

namespace UnitTestProjectMocking1
{
    [TestFixture]
    public class LoggerTest1
    {
        MockSrcubber mockScrubber;
        MockHeader mockHeader;
        MockFooter mockFooter;
        MockSystemConfig mockSystemConfig;

        [SetUp]
        public void SetUp()
        {
            mockScrubber = new MockSrcubber();
            mockHeader = new MockHeader();
            mockFooter = new MockFooter();
            mockSystemConfig = new MockSystemConfig();
            Logger logger = new Logger(mockScrubber, mockHeader, mockFooter, mockSystemConfig);
            logger.Log("My message", LogLevel.Info);
        }

        [Test]
        public void sensitive_data_should_be_scubbed_from_the_log_message()
        {
            Assert.That(mockScrubber.FromWasCalled);
        }

        [Test]
        public void entry_headers_should_be_created()
        {
            Assert.That(mockHeader.ForWasCalled);
        }

        [Test]
        public void entry_footers_should_be_created()
        {
            Assert.That(mockFooter.ForWasCalled);
        }

        [Test]
        public void entry_to_check_stack_trace_should_be_checked()
        {
            Assert.That(mockSystemConfig.LogStackForWasCalled);
        }
    }
}

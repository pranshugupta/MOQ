using ClassLibraryMoq2;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace UnitTestProjectMoq2
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void check_formatter()
        {
            var mockChild = new Mock<ChildClass>();

            mockChild.Object.From(" test    ");

            mockChild.Verify(x => x.ParseBadWords(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void check_formatter_protected()
        {
            var mockChild = new Mock<ChildClass>();
            mockChild.Protected()
                   .Setup<string>("ParseBadWordsProtected", ItExpr.IsAny<string>())
                   .Returns(() => "asdf")
                   .Verifiable();
            mockChild.Object.FromProtected("  sdshd  ");

            mockChild.Verify();


        }
    }
}

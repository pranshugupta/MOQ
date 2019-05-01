using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace UnitTestProject5
{
    [TestClass]
    public class StringAssertTest
    {
        [TestMethod]
        public void Contain()
        {
            string name = "Pranshu Gupta";
            string lastName = "Gupta";
            StringAssert.Contains(name, lastName);
        }

        [TestMethod]
        public void StartsWith()
        {
            string name = "Pranshu Gupta";
            string firstName = "Pranshu";
            StringAssert.StartsWith(name, firstName);
        }
        [TestMethod]
        public void Regexp()
        {
            Regex regex = new Regex(@"^[A-Z]+$");
            string upperCase = "PRANSHU";
            StringAssert.Matches(upperCase, regex);
        }
    }
}

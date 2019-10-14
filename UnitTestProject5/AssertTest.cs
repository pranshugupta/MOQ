using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject5
{
    [TestClass]
    public class AssertTest
    {
        [TestMethod]
        public void FailWithMessage()
        {
            bool result = false;
            Assert.IsTrue(result, "If Fail- you should see this message");
        }

        [TestMethod]
        public void FailWithMessageContained()
        {
            bool result = false;
            string message = "FailWithMessageContained";
            Assert.IsTrue(result, "{0}: If Fail- you should see this message", message);
        }

        [TestMethod]
        public void StringCheckWithIgnoreCase()
        {
            string str1 = "Pranshu";
            string str2 = "pranshu";
            Assert.AreEqual(str1, str2, true);
        }

        [TestMethod]
        public void CheckObjectReference1()
        {
            object obj1 = new object();
            object obj2 = new object();
            Assert.AreNotSame(obj1, obj2);
        }

        [TestMethod]
        public void CheckObjectReference2()
        {
            object obj1 = new object();
            object obj2 = obj1;
            Assert.AreSame(obj1, obj2);
        }

        [TestMethod]
        public void CheckInstance()
        {
            Exception ex = new ArgumentNullException();
            Assert.IsInstanceOfType(ex, typeof(Exception));
        }
    }
}
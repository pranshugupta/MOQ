using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class FileProcessTest1
    {
        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(@"c:\Windows\Regedit.exe");
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(@"c:\DoesNotExists.test");
            Assert.IsFalse(exist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty1()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(string.Empty);
        }


        [TestMethod]
        public void FileNameNullOrEmpty2()
        {
            FileProcess fp = new FileProcess();
            try
            {
                var exist = fp.FileExists(string.Empty);
            }
            catch (ArgumentNullException e)
            {
                return;
            }
            Assert.Fail("Not right exception");
        }
    }
}

using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;

namespace UnitTestProject2
{
    [TestClass]
    public class FileProcessTest2
    {
        private const string BAD_FILE_NAME = @"c:\BadFileName.txt";
        private string GoodFileName;

        // Set before each test is run, useful in data driver testing
        public TestContext TestContext { get; set; }

        public void SetGoodFileName()
        {
            TestContext.WriteLine("Reading app config data");
            GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (GoodFileName.Contains("[AppPath]"))
            {
                GoodFileName = GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void FileNameDoesExist()
        {
            SetGoodFileName();
            File.AppendAllText(GoodFileName, "Pranshu Gupta");

            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(GoodFileName);

            File.Delete(GoodFileName);
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(BAD_FILE_NAME);
            Assert.IsFalse(exist);
        }
    }
}

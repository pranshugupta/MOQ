using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;

namespace UnitTestProject4
{
    [TestClass]
    public class FileProcessTest4
    {
        private const string BAD_FILE_NAME = @"c:\BadFileName.txt";
        private const string FILE_NAME = @"FileToDeploy.txt";
        private string GoodFileName;

        // Set before each test is run, useful in data driver testing
        public TestContext TestContext { get; set; }

        #region Test Initialize and Cleaup
        [TestInitialize]
        public void Test_Initialize()
        {
            TestContext.WriteLine("Inside Test_Initialize: " + TestContext.TestName);
            if (TestContext.TestName == "FileNameDoesExist")
            {
                GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
                if (GoodFileName.Contains("[AppPath]"))
                {
                    GoodFileName = GoodFileName.Replace("[AppPath]",
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    File.AppendAllText(GoodFileName, "Pranshu Gupta");
                }
            }
        }

        [TestCleanup]
        public void Test_Cleanup()
        {
            TestContext.WriteLine("Inside Test_Cleanuo: " + TestContext.TestName);
            if (TestContext.TestName == "FileNameDoesExist")
            {
                File.Delete(GoodFileName);
            }
        }
        #endregion


        [TestMethod]
        [Description("Check if to see if file exist")]  // just provide detail about method
        [Owner("Pranshug")]             // used for grouping
        [TestCategory("File Exist")]    // can be used to run category wise in cmd test utility
        [Priority(1)]                   // can be used to run priority wise in cmd test utility
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(GoodFileName);
            Assert.IsTrue(exist);
        }

        [TestMethod]
        [Description("Check if to see if file does not exist")]
        [Owner("Kummohan")]
        [TestCategory("File not Exist")]    // can be used to run category wise in cmd test utility
        [Priority(0)]                   // can be used to run priority wise in cmd test utility
        // [Ignore]         // Does not run ignored tests
        [Timeout(100)]        // Timeout to run perticular test

        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(BAD_FILE_NAME);
            Assert.IsFalse(exist);

        }

        [DeploymentItem(FILE_NAME)] // this file will be deployed
        public void DeployementFileNameDoesExist()
        {
            string fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;

            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(fileName);
            Assert.IsTrue(exist);
        }
    }
}

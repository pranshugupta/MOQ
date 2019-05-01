using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
/* 
Order or run----
    Initlialization Attributes
1.       AssemblyInitialize: 
            Called once on project.
            Setup resources for all test classes in assembly
2.       ClassyInitialize:
            Once for a class
            Setup resources for all tests in class
3.       TestInitialize:
            Once for each test
            Setup resources for each tests


    Cleanup Attributes
4.       TestCleanup:
            Once after each test
5.       ClassyCleanup:
            Once for all tests in class have run
6.       AssemblyCleanup: 
            Called once all test in assmbly have run


*/
namespace UnitTestProject3
{
    [TestClass]
    public class FileProcessTest3
    {
        private const string BAD_FILE_NAME = @"c:\BadFileName.txt";
        private string GoodFileName;

        #region Class Initialize and Cleaup
        [ClassInitialize]
        public static void Class_Initialize(TestContext tc)
        {
            tc.WriteLine("Inside Class_Initialize");
        }

        [ClassCleanup]
        public static void Class_Cleanup()
        {
            // tc.WriteLine("Inside Class_Cleanup");
        }
        #endregion

        #region Tes Initialize and Cleaup
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


        // Set before each test is run, useful in data driver testing
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            var exist = fp.FileExists(GoodFileName);
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

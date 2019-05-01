using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject6
{
    [TestClass]
    public class FileProcessTest6
    {
        public TestContext TestContext { get; set; }
        [TestMethod]
        [DataSource("System.Data.SqlClient", "ConnectionString", "Schema.TableName", DataAccessMethod.Sequential)]
        public void IfFileNameExistFromDataSource()
        {
            string fileName;
            bool expectedValue;
            bool causesException;


            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"].ToString());


            try
            {
                FileProcess fp = new FileProcess();
                var exist = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, exist);
            }
            catch (AssertFailedException ex)
            {
                throw ex;
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(causesException);
            }

        }
    }
}

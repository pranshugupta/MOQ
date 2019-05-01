using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject3
{
    /// <summary>
    /// Summary description for Assembly_Initialize_Cleanup
    /// </summary>
    [TestClass]
    public class Assembly_Initialize_Cleanup
    {
        [AssemblyInitialize]
        public static void Assembly_Initialize(TestContext tc)
        {
            tc.WriteLine("Inside Assembly_Initialize");
        }

        [AssemblyCleanup]
        public static void Assembly_Cleanup()
        {
            // tc.WriteLine("Inside Assembly_Cleanup");
        }
    }
}

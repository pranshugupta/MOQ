using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject5
{
    [TestClass]
    public class CollectionAssertTest
    {
        [TestMethod]
        public void CompareCollection1()
        {
            var myList = new List<Item>()
            {
                new Item() { ID=1,Name="Pranshu"},
                new Item() { ID=2,Name="Pravesh"}
            };

            CollectionAssert.AreEqual(myList, Item.GetAllItems(), "Fails: because it is check on reference");
        }

        [TestMethod]
        public void CompareCollection2()
        {
            var myList = new List<Item>()
            {
                new Item() { ID=1,Name="Pranshu"},
                new Item() { ID=2,Name="Pravesh"}
            };

            CollectionAssert.AreEqual(myList, Item.GetAllItems(),
                Comparer<Item>.Create((x, y) => x.ID == y.ID && x.Name == y.Name ? 0 : 1)
                );
        }

        [TestMethod]
        public void CompareCollection3()
        {
            var myList = new List<Item>()
            {
                new Item() { ID=2,Name="Pravesh"},
                new Item() { ID=1,Name="Pranshu"}
            };

            CollectionAssert.AreNotEquivalent(myList, Item.GetAllItems());
        }
        [TestMethod]
        public void CheckInstance()
        {

            CollectionAssert.AllItemsAreInstancesOfType(Item.GetAllItems(), typeof(object));
        }
    }
}

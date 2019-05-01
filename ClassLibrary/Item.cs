using System.Collections.Generic;

namespace ClassLibrary
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static List<Item> GetAllItems()
        {
            return new List<Item>()
            {
                new Item() { ID=1,Name="Pranshu"},
                new Item() { ID=2,Name="Pravesh"}
            };
        }
    }
}

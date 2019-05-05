namespace ClassLibraryMoq1
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private string city;

        public Customer(string name, string lastName, string city)
        {
            this.firstName = name;
            this.city = city;
            this.lastName = lastName;
        }

        public Addres MailingAddress { get; internal set; }
        public int Id { get; internal set; }
        public string FullName { get; internal set; }
        public StatusLevel StatusLevel { get; internal set; }
        public int? WorkStationId { get; internal set; }
    }
}
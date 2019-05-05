using System;

namespace ClassLibraryMoq1
{
    public interface ICustomerRepository
    {
        string LocalTimeZone { get; set; }

        void Save(Customer customer);
        void SaveSpecial(Customer customer);

        event EventHandler OnNotify;

        void FetchAll();
    }
}
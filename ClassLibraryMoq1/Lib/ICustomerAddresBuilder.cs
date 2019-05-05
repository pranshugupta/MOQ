namespace ClassLibraryMoq1
{
    public interface ICustomerAddresBuilder
    {
        Addres From(CustomerToCreateDto customerToCreateDto);
        void TryParse(string address, out Addres mailingAddress);
    }
}
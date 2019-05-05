namespace ClassLibraryMoq1
{
    public interface IAddressFormatterFactory
    {
        ICustomerAddresBuilder From(CustomerToCreateDto customerToCreateDto);
    }
}
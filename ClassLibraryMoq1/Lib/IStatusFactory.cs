namespace ClassLibraryMoq1
{
    public interface IStatusFactory
    {
        StatusLevel CreateFrom(CustomerToCreateDto customerToCreateDto);
    }
}
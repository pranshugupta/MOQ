namespace ClassLibraryMoq2
{
    public class ChildClass : BaseClass
    {
        public string From(string data)
        {
            return ParseBadWords(data);
        }
        public string FromProtected(string data)
        {
            return ParseBadWordsProtected(data);
        }
    }
}

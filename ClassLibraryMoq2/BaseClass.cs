namespace ClassLibraryMoq2
{
    public class BaseClass
    {
        public virtual string ParseBadWords(string data)
        {
            return data.Trim();
        }
        protected virtual string ParseBadWordsProtected(string data)
        {
            return data.Trim();
        }
    }
}

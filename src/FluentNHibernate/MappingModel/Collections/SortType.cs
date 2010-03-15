namespace FluentNHibernate.MappingModel.Collections
{
    public class SortType
    {
        public static readonly SortType Unsorted = new SortType("unsorted");
        public static readonly SortType Natural = new SortType("natural");

        readonly string value;

        SortType(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
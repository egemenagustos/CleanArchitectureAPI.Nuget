namespace Core.Persistance.Dyanmic
{
    public class DynamicQuery
    {
        public IEnumerable<Sort> Sorts { get; set; }

        public Filter? Filter { get; set; }

        public DynamicQuery()
        {
        }

        public DynamicQuery(IEnumerable<Sort> sorts, Filter? filter)
        {
            Filter = filter;
            Sorts = sorts;
        }
    }
}

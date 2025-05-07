namespace Ecom.Core.Sharing
{
    public class ProductParams
    {
        public int _maxPageSize { get; set; } = 15;

        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
        }
        public int PageNumber { get; set; } = 1;
        public string Sort { get; set; }
        public Guid? CategoryId { get; set; }

        private string _searchByProductName;

        public string SearchByProductName
        {
            get { return _searchByProductName; }
            set { _searchByProductName = value?.ToLower(); }
        }


    }
}

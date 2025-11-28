
namespace RestApi.Helpers
{
    public class PaginationObject
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int TotalRecords { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)System.Math.Ceiling((double)TotalRecords / PageSize);
            }
        }


    }
}
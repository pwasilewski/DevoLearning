namespace Nihdi.DevoLearning.Contracts.Shared.Pagination
{
    public class PaginatedResultDto<T>
    {
        public List<T> Items
        {
            get; set;
        }

        public int PageIndex
        {
            get; set;
        }

        public int PageSize
        {
            get; set;
        }

        public int FilteredResultCount
        {
            get; set;
        }

        public int UnfilteredResultCount
        {
            get; set;
        }
    }
}

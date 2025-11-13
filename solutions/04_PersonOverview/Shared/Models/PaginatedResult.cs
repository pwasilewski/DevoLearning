namespace Nihdi.DevoLearning.Presentation.Shared.Models
{
    public class PaginatedResult<T>
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

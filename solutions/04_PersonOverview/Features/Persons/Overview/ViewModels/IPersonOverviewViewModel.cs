namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ViewModels
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public interface IPersonOverviewViewModel
    {
        bool IsLoading
        {
            get;
        }

        PersonOverviewQuery Query
        {
            get;
        }

        PaginatedResult<PersonOverviewModel> PaginatedResult
        {
            get;
        }

        Task OnInitializedAsync(IErrorComponent errorComponent);

        Task SearchAsync();
    }
}

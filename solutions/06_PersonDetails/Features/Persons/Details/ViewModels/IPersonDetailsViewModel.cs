namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.ViewModels
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;
    using Nihdi.DevoLearning.Presentation.Shared;

    public interface IPersonDetailsViewModel
    {
        bool IsLoading
        {
            get;
        }

        PersonDetailsModel Person
        {
            get;
        }

        Task OnInitializedAsync(IErrorComponent errorComponent, int id);
    }
}

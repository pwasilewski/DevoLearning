namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.ViewModels
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

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

        IReadOnlyList<GenderModel> Genders
        {
            get;
        }

        IReadOnlyList<CivilStateModel> CivilStates
        {
            get;
        }

        Task OnInitializedAsync(IErrorComponent errorComponent, int id);
    }
}

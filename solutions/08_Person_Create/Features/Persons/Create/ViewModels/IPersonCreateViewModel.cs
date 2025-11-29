namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.ViewModels
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public interface IPersonCreateViewModel
    {
        bool IsLoading
        {
            get;
        }

        bool IsSubmitting
        {
            get;
        }

        PersonCreateModel Model
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

        int? CreatedPersonId
        {
            get;
        }

        Task OnInitializedAsync(IErrorComponent errorComponent);

        Task SubmitAsync();
    }
}

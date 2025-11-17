namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.ViewModels
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients;
    using Nihdi.DevoLearning.Presentation.Services.CivilStates;
    using Nihdi.DevoLearning.Presentation.Services.Genders;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class PersonDetailsViewModel(
        IPersonDetailsServiceClient serviceClient,
        IGenderLookupService genderLookupService,
        ICivilStateLookupService civilStateLookupService) : IPersonDetailsViewModel
    {
        public bool IsLoading
        {
            get; private set;
        }

        public PersonDetailsModel Person
        {
            get; private set;
        }

        public IReadOnlyList<GenderModel> Genders
        {
            get; private set;
        }

        public IReadOnlyList<CivilStateModel> CivilStates
        {
            get; private set;
        }

        public async Task OnInitializedAsync(IErrorComponent errorComponent, int id)
        {
            IsLoading = true;

            try
            {
                Person = await serviceClient.GetPersonByIdAsync(id);
                Genders = await genderLookupService.GetAsync();
                CivilStates = await civilStateLookupService.GetAsync();
            }
            catch (Exception ex)
            {
                errorComponent.ProcessError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

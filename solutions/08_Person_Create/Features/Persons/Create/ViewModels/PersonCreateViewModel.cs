namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.ServiceClients;
    using Nihdi.DevoLearning.Presentation.Services.CivilStates;
    using Nihdi.DevoLearning.Presentation.Services.Genders;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class PersonCreateViewModel(
        IGenderLookupService genderLookupService,
        ICivilStateLookupService civilStateLookupService,
        IPersonCreateServiceClient serviceClient) : IPersonCreateViewModel
    {
        private IErrorComponent _errorComponent;

        public bool IsLoading
        {
            get; private set;
        }

        public bool IsSubmitting
        {
            get; private set;
        }

        public PersonCreateModel Model
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

        public int? CreatedPersonId
        {
            get; private set;
        }

        public async Task OnInitializedAsync(IErrorComponent errorComponent)
        {
            _errorComponent = errorComponent;
            IsLoading = true;

            try
            {
                Genders = await genderLookupService.GetAsync();
                CivilStates = await civilStateLookupService.GetAsync();

                Model = new PersonCreateModel();
            }
            catch (Exception ex)
            {
                _errorComponent.ProcessError(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task SubmitAsync()
        {
            IsSubmitting = true;

            try
            {
                CreatedPersonId = await serviceClient.CreateAsync(Model);
            }
            catch (Exception ex)
            {
                _errorComponent.ProcessError(ex);
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}

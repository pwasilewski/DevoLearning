namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ViewModels
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class PersonOverviewViewModel : IPersonOverviewViewModel
    {
        private readonly IPersonOverviewServiceClient _serviceClient;
        private IErrorComponent _errorComponent;

        public PersonOverviewViewModel(IPersonOverviewServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public bool IsLoading
        {
            get; private set;
        }

        public PersonOverviewQuery Query
        {
            get; private set;
        }

        public PaginatedResult<PersonOverviewModel> PaginatedResult
        {
            get; private set;
        }

        public async Task OnInitializedAsync(IErrorComponent errorComponent)
        {
            // Store errorComponent
            _errorComponent = errorComponent;

            // Initialize Query (defaults)
            Query = new PersonOverviewQuery
            {
                PageIndex = 0,
                PageSize = 10,
            };

            await SearchAsync();
        }

        public async Task SearchAsync()
        {
            IsLoading = true;

            try
            {
                PaginatedResult = await _serviceClient.GetPersonsAsync(Query);
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
    }
}

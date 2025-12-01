namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ViewModels
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class PersonOverviewViewModel(IPersonOverviewServiceClient serviceClient) : IPersonOverviewViewModel
    {
        private IErrorComponent _errorComponent;

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
                PaginatedResult = await serviceClient.GetPersonsAsync(Query);
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
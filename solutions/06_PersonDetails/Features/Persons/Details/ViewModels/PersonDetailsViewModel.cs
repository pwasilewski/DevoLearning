namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.ViewModels
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients;
    using Nihdi.DevoLearning.Presentation.Shared;

    public class PersonDetailsViewModel(IPersonDetailsServiceClient serviceClient) : IPersonDetailsViewModel
    {
        public bool IsLoading
        {
            get; private set;
        }

        public PersonDetailsModel Person
        {
            get; private set;
        }

        public async Task OnInitializedAsync(IErrorComponent errorComponent, int id)
        {
            IsLoading = true;

            try
            {
                Person = await serviceClient.GetPersonByIdAsync(id);
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

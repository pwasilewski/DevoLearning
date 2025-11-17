namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;

    public interface IPersonDetailsServiceClient
    {
        Task<PersonDetailsModel> GetPersonByIdAsync(int id);
    }
}
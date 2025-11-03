namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public interface IPersonOverviewServiceClient
    {
        Task<PaginatedResult<PersonOverviewModel>> GetPersonsAsync(PersonOverviewQuery query);
    }
}

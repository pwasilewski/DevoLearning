namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.ServiceClients
{
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;

    public interface IPersonCreateServiceClient
    {
        Task<int> CreateAsync(PersonCreateModel model);
    }
}

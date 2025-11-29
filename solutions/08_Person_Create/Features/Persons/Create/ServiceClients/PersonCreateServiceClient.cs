namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.ServiceClients
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;

    public class PersonCreateServiceClient : IPersonCreateServiceClient
    {
        public Task<int> CreateAsync(PersonCreateModel model)
        {
            return Task.FromResult(42);
        }
    }
}

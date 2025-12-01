namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.ServiceClients
{
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ServiceClients;

    public class PersonDetailsServiceClient : IPersonDetailsServiceClient
    {
        private readonly PersonDetailsModel _mock = new()
        {
            Id = 42,
            LastName = "Vermeer",
            FirstName = "Alice",
            BirthDate = new DateTime(1992, 4, 10),
            DeceasedDate = null,
            GenderId = 2,
            CivilStateId = 1,
            Email = "alice.vermeer@example.com",
            Mobile = "+32 475 11 22 33",
            CreatedOn = new DateTime(2020, 1, 15, 10, 30, 0),
            CreatedBy = "Migration",
            ModifiedOn = new DateTime(2020, 1, 15, 10, 30, 0),
            ModifiedBy = "Migration",
        };

        public async Task<PersonDetailsModel> GetPersonByIdAsync(int id)
        {
            await Task.Delay(1000);
            return _mock;
        }
    }
}

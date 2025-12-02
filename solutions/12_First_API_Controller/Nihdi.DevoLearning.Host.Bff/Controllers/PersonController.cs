namespace Nihdi.DevoLearning.Host.Bff.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Nihdi.DevoLearning.Contracts.Persons.Details;

    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetPersonDetailsById(int id)
        {
            var result = new PersonDetailsDto()
            {
                Id = id,
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

            return Ok(result);
        }
    }
}

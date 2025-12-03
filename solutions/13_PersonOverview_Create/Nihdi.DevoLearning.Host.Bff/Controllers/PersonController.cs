namespace Nihdi.DevoLearning.Host.Bff.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Nihdi.DevoLearning.Contracts.Persons.Create;
    using Nihdi.DevoLearning.Contracts.Persons.Details;
    using Nihdi.DevoLearning.Contracts.Persons.Overview;
    using Nihdi.DevoLearning.Contracts.Shared.Pagination;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly List<PersonOverviewDto> _mockData =
        [
            new() { Id = 1, FirstName = "Alice", LastName = "Vermeer", BirthDate = new DateTime(1992, 4, 10), Email = "alice.vermeer@example.com" },
            new() { Id = 2, FirstName = "Benoît", LastName = "Durand", BirthDate = new DateTime(1988, 6, 21), Email = "benoit.durand@example.com" },
            new() { Id = 3, FirstName = "Clara", LastName = "Peeters", BirthDate = new DateTime(1995, 12, 5), Email = "clara.peeters@example.com" },
            new() { Id = 4, FirstName = "David", LastName = "De Smet", BirthDate = new DateTime(1990, 1, 15), Email = "david.desmet@example.com" },
            new() { Id = 5, FirstName = "Emma", LastName = "Lefèvre", BirthDate = new DateTime(1993, 8, 30), Email = "emma.lefevre@example.com" },
            new() { Id = 6, FirstName = "Felix", LastName = "Dupont", BirthDate = new DateTime(1985, 2, 13), Email = "felix.dupont@example.com" },
            new() { Id = 7, FirstName = "Greta", LastName = "Van Acker", BirthDate = new DateTime(1991, 3, 25), Email = "greta.vanacker@example.com" },
            new() { Id = 8, FirstName = "Hugo", LastName = "Lemaire", BirthDate = new DateTime(1994, 9, 1), Email = "hugo.lemaire@example.com" },
            new() { Id = 9, FirstName = "Inès", LastName = "Declercq", BirthDate = new DateTime(1997, 7, 12), Email = "ines.declercq@example.com" },
            new() { Id = 10, FirstName = "Jules", LastName = "Martens", BirthDate = new DateTime(1989, 11, 9), Email = "jules.martens@example.com" },
            new() { Id = 11, FirstName = "Karen", LastName = "De Wilde", BirthDate = new DateTime(1986, 5, 17), Email = "karen.dewilde@example.com" },
            new() { Id = 12, FirstName = "Liam", LastName = "Janssens", BirthDate = new DateTime(1998, 10, 3), Email = "liam.janssens@example.com" },
            new() { Id = 13, FirstName = "Maya", LastName = "Moreau", BirthDate = new DateTime(1992, 2, 28), Email = "maya.moreau@example.com" },
            new() { Id = 14, FirstName = "Noah", LastName = "Verhoeven", BirthDate = new DateTime(1996, 12, 19), Email = "noah.verhoeven@example.com" },
            new() { Id = 15, FirstName = "Olivia", LastName = "Lambert", BirthDate = new DateTime(1993, 4, 22), Email = "olivia.lambert@example.com" },
            new() { Id = 16, FirstName = "Pablo", LastName = "Garcia", BirthDate = new DateTime(1987, 7, 7), Email = "pablo.garcia@example.com" },
            new() { Id = 17, FirstName = "Quinn", LastName = "Dubois", BirthDate = new DateTime(1999, 1, 31), Email = "quinn.dubois@example.com" },
            new() { Id = 18, FirstName = "Rafael", LastName = "Costa", BirthDate = new DateTime(1991, 6, 11), Email = "rafael.costa@example.com" },
            new() { Id = 19, FirstName = "Sofia", LastName = "Ricci", BirthDate = new DateTime(1995, 3, 9), Email = "sofia.ricci@example.com" },
            new() { Id = 20, FirstName = "Thomas", LastName = "Nguyen", BirthDate = new DateTime(1988, 9, 27), Email = "thomas.nguyen@example.com" },
            new() { Id = 21, FirstName = "Uma", LastName = "Kovacs", BirthDate = new DateTime(1990, 12, 14), Email = "uma.kovacs@example.com" },
            new() { Id = 22, FirstName = "Victor", LastName = "Mueller", BirthDate = new DateTime(1984, 8, 5), Email = "victor.mueller@example.com" },
            new() { Id = 23, FirstName = "Wiktor", LastName = "Nowak", BirthDate = new DateTime(1996, 1, 20), Email = "wiktor.nowak@example.com" },
            new() { Id = 24, FirstName = "Xenia", LastName = "Petrova", BirthDate = new DateTime(1993, 11, 16), Email = "xenia.petrova@example.com" },
            new() { Id = 25, FirstName = "Yara", LastName = "Haddad", BirthDate = new DateTime(1997, 5, 2), Email = "yara.haddad@example.com" },
        ];

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

        [HttpGet]
        public IActionResult GetPersons([FromQuery] PersonOverviewQueryDto query)
        {
            var pagedItems = _mockData
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            return Ok(new PaginatedResultDto<PersonOverviewDto>
            {
                Items = pagedItems,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                FilteredResultCount = _mockData.Count,
                UnfilteredResultCount = _mockData.Count
            });
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonCreateDto dto)
        {
            var id = new Random().Next(1000);
            return Ok(id);
        }
    }
}

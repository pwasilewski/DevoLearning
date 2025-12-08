namespace Nihdi.DevoLearning.Host.Bff.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Nihdi.DevoLearning.Contracts.Referential;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class ReferentialController : ControllerBase
    {
        private static readonly IReadOnlyList<GenderDto> _genders =
        [
            new() { Id = 1, Name = new() { ValueFr = "Masculin", ValueNl = "Mannelijk" } },
            new() { Id = 2, Name = new() { ValueFr = "Féminin", ValueNl = "Vrouwelijk" } },
            new() { Id = 3, Name = new() { ValueFr = "X", ValueNl = "X" } },
        ];

        private static readonly IReadOnlyList<CivilStateDto> _states =
        [
            new() { Id = 1, Name = new() { ValueFr = "Célibataire", ValueNl = "Ongehuwd" } },
            new() { Id = 2, Name = new() { ValueFr = "Marié", ValueNl = "Gehuwd" } },
            new() { Id = 3, Name = new() { ValueFr = "Veuf/veuve", ValueNl = "Weduwnaar/weduwe" } },
            new() { Id = 4, Name = new() { ValueFr = "Divorcé", ValueNl = "Gescheiden" } },
            new() { Id = 5, Name = new() { ValueFr = "Séparé de corps et de biens", ValueNl = "Scheiding van tafel en bed en van goederen" } },
            new() { Id = 6, Name = new() { ValueFr = "Dissolution du mariage sous une forme particulière", ValueNl = "Ontbinding van het huwelijk op een bijzondere wijze" } },
            new() { Id = 7, Name = new() { ValueFr = "Partenariat", ValueNl = "Partnerschap" } },
            new() { Id = 8, Name = new() { ValueFr = "Indéterminé", ValueNl = "Onbepaald" } }
        ];

        [HttpGet("genders")]
        public ActionResult<List<GenderDto>> GetGenders()
        {
            return Ok(_genders);
        }

        [HttpGet("civilstates")]
        public ActionResult<List<CivilStateDto>> GetCivilStates()
        {
            return Ok(_states);
        }
    }
}

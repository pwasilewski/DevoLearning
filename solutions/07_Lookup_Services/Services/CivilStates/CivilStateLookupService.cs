namespace Nihdi.DevoLearning.Presentation.Services.CivilStates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class CivilStateLookupService : ICivilStateLookupService
    {
        private static readonly IReadOnlyList<CivilStateModel> _states =
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

        public Task<IReadOnlyList<CivilStateModel>> GetAsync()
        {
            return Task.FromResult(_states);
        }
    }
}

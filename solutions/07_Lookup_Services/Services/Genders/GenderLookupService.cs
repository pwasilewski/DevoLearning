namespace Nihdi.DevoLearning.Presentation.Services.Genders
{
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public class GenderLookupService : IGenderLookupService
    {
        private static readonly IReadOnlyList<GenderModel> _genders =
        [
            new() { Id = 0, Name = new LocalizedStringModel { ValueFr = "Masculin", ValueNl = "Mannelijk" } },
            new() { Id = 1, Name = new LocalizedStringModel { ValueFr = "Féminin", ValueNl = "Vrouwelijk" } },
            new() { Id = 2, Name = new LocalizedStringModel { ValueFr = "X", ValueNl = "X" } },
        ];

        public Task<IReadOnlyList<GenderModel>> GetAsync()
        {
            return Task.FromResult(_genders);
        }
    }
}

namespace Nihdi.DevoLearning.Presentation.Services.Genders
{
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public interface IGenderLookupService
    {
        Task<IReadOnlyList<GenderModel>> GetAsync();
    }
}

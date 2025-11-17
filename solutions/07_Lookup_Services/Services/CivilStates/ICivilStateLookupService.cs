namespace Nihdi.DevoLearning.Presentation.Services.CivilStates
{
    using Nihdi.DevoLearning.Presentation.Shared.Models;

    public interface ICivilStateLookupService
    {
        Task<IReadOnlyList<CivilStateModel>> GetAsync();
    }
}

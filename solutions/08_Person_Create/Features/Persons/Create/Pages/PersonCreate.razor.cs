namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.Pages
{
    using Microsoft.AspNetCore.Components;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.ViewModels;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Navigation;

    public partial class PersonCreate
    {
        [CascadingParameter]
        public Error Error
        {
            get; set;
        }

        [Inject]
        public IPersonCreateViewModel ViewModel
        {
            get; set;
        }

        [Inject]
        public INavigationService NavigationService
        {
            get; set;
        }

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.OnInitializedAsync(Error);
        }
    }
}

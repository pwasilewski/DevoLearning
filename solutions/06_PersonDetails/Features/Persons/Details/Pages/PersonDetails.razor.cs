namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.Pages
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Details.ViewModels;
    using Nihdi.DevoLearning.Presentation.Shared;

    public partial class PersonDetails
    {
        [CascadingParameter]
        public Error ErrorComponent
        {
            get; set;
        }

        [Inject]
        public IPersonDetailsViewModel ViewModel
        {
            get; set;
        }

        [Parameter]
        public int Id
        {
            get; set;
        }

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.OnInitializedAsync(ErrorComponent, Id);
        }
    }
}

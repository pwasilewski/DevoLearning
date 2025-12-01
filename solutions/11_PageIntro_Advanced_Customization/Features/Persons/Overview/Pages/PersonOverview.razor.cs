namespace Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Pages
{
    using Microsoft.AspNetCore.Components;
    using MudBlazor;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.Models;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Overview.ViewModels;
    using Nihdi.DevoLearning.Presentation.Shared;
    using Nihdi.DevoLearning.Presentation.Shared.Navigation;

    public partial class PersonOverview
    {
        [CascadingParameter]
        public Error ErrorComponent
        {
            get; set;
        }

        [Inject]
        public IPersonOverviewViewModel ViewModel
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
            await ViewModel.OnInitializedAsync(ErrorComponent);
        }

        private async Task<GridData<PersonOverviewModel>> ServerReload(GridState<PersonOverviewModel> state)
        {
            ViewModel.Query.PageIndex = state.Page;
            ViewModel.Query.PageSize = state.PageSize;

            await ViewModel.SearchAsync();

            var paginatedResult = ViewModel.PaginatedResult;
            return new GridData<PersonOverviewModel>
            {
                TotalItems = paginatedResult?.UnfilteredResultCount ?? 0,
                Items = paginatedResult?.Items ?? [],
            };
        }

        private void NavigateToDetails(DataGridRowClickEventArgs<PersonOverviewModel> args)
        {
            NavigationService.NavigateTo($"/Persons/{args.Item.Id}/Details");
        }

        private void NavigateToForm()
        {
            NavigationService.NavigateTo(Routing.Persons.Create);
        }
    }
}
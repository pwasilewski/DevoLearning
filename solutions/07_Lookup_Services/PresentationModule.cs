// <copyright file="PresentationModule.cs" company="Riziv-Inami">
// Copyright (c) Riziv-Inami. All rights reserved.
// </copyright>

namespace Nihdi.DevoLearning.Presentation
{
    using Microsoft.Extensions.DependencyInjection;
    using Nihdi.DevoLearning.Presentation.Services.CivilStates;
    using Nihdi.DevoLearning.Presentation.Services.Genders;
    using Nihdi.DevoLearning.Presentation.Shared.Navigation;

    public class PresentationModule
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            // ViemModels convention
            services.Scan(scan => scan.FromAssemblyOf<PresentationModule>()
            .AddClasses(classes => classes.Where(t => t.Name.EndsWith("ViewModel")))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            .AddClasses(classes => classes.Where(t => t.Name.EndsWith("ServiceClient")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            // NavigationService
            services.AddScoped<INavigationService, NavigationService>();

            services.AddScoped<IGenderLookupService, GenderLookupService>();
            services.AddScoped<ICivilStateLookupService, CivilStateLookupService>();
        }
    }
}
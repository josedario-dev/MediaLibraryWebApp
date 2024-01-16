using MediaLibrary.WebApp.Client;
using MediaLibrary.WebApp.Common.MessagingServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MediaLibrary.WebApp.Common.Helpers;
using MediaLibrary.WebApp.Common.Helpers.Contracts;
using MediaLibrary.WebApp.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using MediaLibrary.WebApp.Client.Repositories.Contracts;
using MediaLibrary.WebApp.Client.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;
using MediaLibrary.WebApp.Shared;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// El singleton es para que la instancia permanezca en en el ciclo de vida de la app
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Este patrón Singleton permite que las clases se mantengan la instancia en todo el ciclo de vida de la App
builder.Services.AddSingleton<IAppState, AppState>();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();


// Services
builder.Services.AddTransient<IMudMessagingService, MudMessagingService>();
builder.Services.AddTransient<IUploadFiles, UploadFiles>();
builder.Services.AddTransient<IHelpers, Helpers>();
await builder.Build().RunAsync();
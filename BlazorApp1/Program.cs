using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using BlazorApp1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// OIDC Authentication with your custom settings
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "http://localhost:5003";
    options.ProviderOptions.ClientId = "api-gateway";
    options.ProviderOptions.RedirectUri = "http://localhost:5059/authentication/login-callback";
    options.ProviderOptions.ResponseType = "code"; // Authorization Code Flow with PKCE
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("roles");
    options.ProviderOptions.DefaultScopes.Add("vacancy");
    options.ProviderOptions.DefaultScopes.Add("offline_access"); // Enables refresh tokens
    options.ProviderOptions.DefaultScopes.Add("profile_extended");
    // options.ProviderOptions.AdditionalProviderParameters.Add("code_challenge_method", "S256"); // PKCE
});

// Configure HttpClient for secure API calls
builder.Services.AddHttpClient("ServerAPI", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Register named HttpClient for dependency injection
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

await builder.Build().RunAsync();
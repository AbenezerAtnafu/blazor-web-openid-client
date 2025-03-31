using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthenticationMessageHandler>();
builder.Services.AddHttpClient("api", opt => opt.BaseAddress =  new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CustomAuthenticationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(opt =>
{
    opt.ProviderOptions.Authority = "http://localhost:5032/identity";
    opt.ProviderOptions.ClientId = "test-blazor";
    opt.ProviderOptions.ResponseType = "code";
    opt.ProviderOptions.DefaultScopes.Add("openid");
    opt.ProviderOptions.DefaultScopes.Add("email");
    opt.ProviderOptions.DefaultScopes.Add("profile");
    opt.ProviderOptions.DefaultScopes.Add("roles");
    opt.ProviderOptions.DefaultScopes.Add("vacancy");
    opt.ProviderOptions.DefaultScopes.Add("offline_access");
    opt.ProviderOptions.AdditionalProviderParameters.Add("code_challenge_method", "S256");
});

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorApp1;

public class CustomAuthenticationMessageHandler:AuthorizationMessageHandler
{
    public CustomAuthenticationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
    {
        ConfigureHandler(new string[] {"http://localhost:5059/"});
    }
}
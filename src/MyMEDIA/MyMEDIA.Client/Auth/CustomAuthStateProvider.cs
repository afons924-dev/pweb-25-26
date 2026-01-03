using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyMEDIA.Client.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync("authToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    public async Task Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("login", new { email, password });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result != null && !string.IsNullOrEmpty(result.AccessToken))
            {
                await _localStorage.SetItemAsync("authToken", result.AccessToken);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Register(string email, string password)
    {
         await _httpClient.PostAsJsonAsync("register", new { email, password });
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}

public interface ILocalStorageService
{
    Task<string> GetItemAsync(string key);
    Task SetItemAsync(string key, string value);
    Task RemoveItemAsync(string key);
}

// Simple in-memory implementation for PoC (or use Blazored.LocalStorage)
public class BrowserLocalStorage : ILocalStorageService
{
    private readonly Microsoft.JSInterop.IJSRuntime _jsRuntime;
    public BrowserLocalStorage(Microsoft.JSInterop.IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

    public async Task<string> GetItemAsync(string key) => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
    public async Task SetItemAsync(string key, string value) => await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    public async Task RemoveItemAsync(string key) => await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
}

public class LoginResult
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}

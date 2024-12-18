﻿using Microsoft.JSInterop;
using System.Net.Http.Json;


namespace Presentation.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;


    public UserService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var loginData = new { Username = username, Password = password };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5242/api/User/login", loginData);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();

         
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);

            return true;
        }

        return false;
    }
}
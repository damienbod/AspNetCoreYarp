﻿using Microsoft.Identity.Web;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace UIApp;

public class LegacyViaProxyService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ITokenAcquisition _tokenAcquisition;
    private readonly IConfiguration _configuration;

    public LegacyViaProxyService(IHttpClientFactory clientFactory,
        ITokenAcquisition tokenAcquisition,
        IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _tokenAcquisition = tokenAcquisition;
        _configuration = configuration;
    }

    public async Task<JArray> GetApiDataAsync()
    {
        var client = _clientFactory.CreateClient();

        var scope = _configuration["AspNetCoreProxy:ScopeForAccessToken"];
        if (scope == null) throw new ArgumentException(nameof(scope));

        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope });

        client.BaseAddress = new Uri(_configuration["AspNetCoreProxy:ApiBaseAddress"]!);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("api/crazy");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JArray.Parse(responseContent);

            return data;
        }

        throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
    }
}
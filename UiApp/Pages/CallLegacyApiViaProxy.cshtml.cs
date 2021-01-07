﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace UIApp.Pages
{
    public class CallApiModel : PageModel
    {
        private readonly LegacyViaProxyService _apiService;

        public JArray DataFromApi { get; set; }
        public CallApiModel(LegacyViaProxyService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            DataFromApi = await _apiService.GetApiDataAsync();
        }
    }
}
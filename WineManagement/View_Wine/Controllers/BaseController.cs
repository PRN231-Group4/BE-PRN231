using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Net.Http.Headers;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string WineURL;

        public BaseController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            var apiSettings = new ApiSettings();
            configuration.GetSection("ApiUrls").Bind(apiSettings);
            WineURL = apiSettings.WineURL;

        }
    }
}

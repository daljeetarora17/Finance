using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using NDFinance.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NDFinance.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["UserIsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginVM loginVM)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
{
{ "Username", loginVM.Username },
{ "Password", loginVM.Password}
};

                client.BaseAddress = new Uri("http://localhost:52667/");
                var content = new FormUrlEncodedContent(values);
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = client.PostAsync("api/login", content).Result;
                var value = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<dynamic>(value);

                if (response.IsSuccessStatusCode)
                {
                    return View("/Home/Index");
                }

                return View();
            }

        }
    }
}

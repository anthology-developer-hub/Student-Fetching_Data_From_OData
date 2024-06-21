using FetchingDataFrom_Odata.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace FetchingDataFrom_Odata.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Odata_Value(string username, string password, string Values)
        {

            string Username = username;
            string Password = password;

            string auth = $"{Username}:{Password}";
            var bytes = Encoding.UTF8.GetBytes(auth);
            var base64 = Convert.ToBase64String(bytes);


            using (var client = new HttpClient())
            {
           
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);

                var postTask = client.GetAsync(Values).GetAwaiter().GetResult();

                var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return result;

            }


        }
    }
}

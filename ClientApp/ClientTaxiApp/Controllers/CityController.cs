using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class CityController : BaseWebController
    {
        public CityController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }
        public async Task<IActionResult> CityIndex()
        {
            var apiUrl = _options.ApiUrl + "/City";


            var cityresponce = await _httpClient.GetStringAsync(apiUrl);

            var citylist = JsonConvert.DeserializeObject<IEnumerable<Cities>>(cityresponce);

            return View(citylist);
        }
        public async Task<IActionResult> CityDetails(int? Id)

        {

            var apiUrl = _options.ApiUrl + "/City/" + Id;
            var cityres = await _httpClient.GetStringAsync(apiUrl);
         
            var citytodo = JsonConvert.DeserializeObject<Cities>(cityres);
            Console.WriteLine(citytodo);
            return View(citytodo);
        }
        public async Task<IActionResult> CityDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/City/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }

        [HttpGet]
        public IActionResult CityPut()
        {

            return View();
        }

        public async Task<IActionResult> CityPut(int? Id, CityViewModel Cityt)
        {
            var apiUrl = _options.ApiUrl + "/City/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, Cityt);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(CityIndex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
       

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel city)
        {

            var ApiUrl = _options.ApiUrl + "/City";


            var cityres = await _httpClient.PostAsJsonAsync(ApiUrl, city);



            if (cityres.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return RedirectToAction(nameof(CityIndex));
               // return Redirect(Url);

            }
            else
            {
                ModelState.AddModelError("", "Failed to enter new City");
            }
            return View();

        }

    }
}

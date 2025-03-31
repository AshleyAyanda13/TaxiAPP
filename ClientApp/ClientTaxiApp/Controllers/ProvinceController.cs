using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class ProvinceController : BaseWebController
    {
        public ProvinceController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> Provinceindex()
        {
            var apiUrl = _options.ApiUrl + "/Province";


            var provinceresponce = await _httpClient.GetStringAsync(apiUrl);

            var provincelist = JsonConvert.DeserializeObject<IEnumerable<Province>>(provinceresponce);

            return View(provincelist);
        }

        public async Task<IActionResult> ProvinceDetails(int? Id)

        {
            var apiUrl = _options.ApiUrl + "/Province/" + Id;
            var provinceres = await _httpClient.GetStringAsync(apiUrl);
            var provincetodo = JsonConvert.DeserializeObject<Province>(provinceres);

            return View(provincetodo);
        }
        public async Task<IActionResult> ProvinceDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Province/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProvinceModelView province)
        {

            var ApiUrl = _options.ApiUrl + "/Province";


            var provinceres = await _httpClient.PostAsJsonAsync(ApiUrl, province);



            if (provinceres.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return RedirectToAction(nameof(Provinceindex));
               // return Redirect(Url);


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter new Province");
            }
            return View();

        }

        [HttpGet]
        public IActionResult ProvincePut()
        {

            return View();
        }

        public async Task<IActionResult> ProvincePut(int? Id, ProvinceViewModel provincet)
        {
            var apiUrl = _options.ApiUrl + "/Province/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, provincet);


            if (marshalresponce.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Provinceindex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
            

        }

    }
}

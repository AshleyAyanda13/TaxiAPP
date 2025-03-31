using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class DriverController : BaseWebController
    {
        public DriverController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> DriverIndex()
        {
            var apiUrl = _options.ApiUrl + "/Driver";


            var driverresponce = await _httpClient.GetStringAsync(apiUrl);

            var driverlist = JsonConvert.DeserializeObject<IEnumerable<Driver>>(driverresponce);

            return View(driverlist);
        }
        public async Task<IActionResult> DriverDetails(int? Id)

        {

            var apiUrl = _options.ApiUrl + "/Driver/" + Id;
            var driverres=await _httpClient.GetStringAsync(apiUrl);
            var drivertodo = JsonConvert.DeserializeObject<Driver>(driverres);

            return View(drivertodo);

            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DriverViewModel driver)
        {

            var ApiUrl = _options.ApiUrl + "/Driver";

          //  var driverinsert = JsonConvert.SerializeObject(driver);
        var driverres = await _httpClient.PostAsJsonAsync(ApiUrl,driver);



            if (driverres.IsSuccessStatusCode)
            {


                var Url = _options.ApiUrl + "/Account/Login";
                return RedirectToAction(nameof(DriverIndex));
                // return Redirect(Url);

               // return Redirect(_options.ApiUrl + "/Account/Login");
              //  return RedirectToAction(nameof(DriverIndex));


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter new driver");
            }
            return View();

        }



        public async Task<IActionResult> DriverDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Driver/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }

        [HttpGet]
        public IActionResult DriverPut()
        {

            return View();
        }

        public async Task<IActionResult> DriverPut(int? Id, DriverViewModel drivert)
        {
            var apiUrl = _options.ApiUrl + "/Driver/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, drivert);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(DriverIndex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
         

        }


    }
}

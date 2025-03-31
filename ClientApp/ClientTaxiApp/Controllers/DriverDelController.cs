using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class DriverDelController : BaseWebController
    {
        public DriverDelController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> DriverDelIndex()
        {
            var apiUrl = _options.ApiUrl + "/DriverDel";


            var driverdelresponce = await _httpClient.GetStringAsync(apiUrl);


            var driverdellist = JsonConvert.DeserializeObject<IEnumerable<DriverDel>>(driverdelresponce);

            return View(driverdellist);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DriverDelViewModel Driverdel)
        {

            var ApiUrl = _options.ApiUrl + "/DriverDel";


            var delres = await _httpClient.PostAsJsonAsync(ApiUrl, Driverdel);



            if (delres.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return Redirect(Url);


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter to enter to Driver Delete");
            }
            return View();

        }














        public async Task<IActionResult> DriverDelDetails(int? id)

        {

            var apiUrl = _options.ApiUrl + "/DriverDel/" + id;
            var driverdelres = await _httpClient.GetStringAsync(apiUrl);
            var driverdeltodo = JsonConvert.DeserializeObject<DriverDel>(driverdelres);

            return View(driverdeltodo);

        }
        public async Task<IActionResult> DriverDelDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/DriverDel/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }


        [HttpGet]
        public IActionResult DriverDelPut()
        {

            return View();
        }
        public async Task<IActionResult> DriverDelPut(int? Id, DriverDelViewModel Driverdel)
        {
            var apiUrl = _options.ApiUrl + "/DriverDel/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, Driverdel);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(DriverDelIndex));
            }
            else
           

                ModelState.AddModelError("", "Failed to edit records!");
                ModelState.AddModelError("", "Make sure id of Driver is valid");
                return View();
      

        }
    }
}

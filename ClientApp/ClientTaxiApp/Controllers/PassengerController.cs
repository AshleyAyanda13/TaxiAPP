using ClientTaxiApp.Models;
using ClientTaxiApp.Models.ViewModels;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class PassengerController : BaseWebController
    {
        public PassengerController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> PassengerIndex()
        {
            var apiUrl = _options.ApiUrl + "/Passenger";


            var passengerresponce = await _httpClient.GetStringAsync(apiUrl);




            var passengerlist = JsonConvert.DeserializeObject<Passenger>(passengerresponce);

            return View(passengerlist);
        }
        public async Task<IActionResult> PassengerDetails(int? Id)

        {

            var apiUrl = _options.ApiUrl + "/Passenger/" + Id;
            var passengerres = await _httpClient.GetStringAsync(apiUrl);
            var passengertodo = JsonConvert.DeserializeObject<Passenger>(passengerres);

            return View(passengertodo);


        }


        //create new passenger profile 
        [HttpPost]
        public async Task<IActionResult> Create(PassengerViewModel passengerViewModel)
        {

            var apiUrl = _options.ApiUrl + "/Passenger";


            //fetch logged in user data
            var loggedUserUrl = _options.ApiUrl + "/Account/Login";
            var res = await _httpClient.GetStringAsync(loggedUserUrl);
            var user = JsonConvert.DeserializeObject<AppUser>(res);

            passengerViewModel.AppUserId = user.Id;

            if (passengerViewModel.Numbers.Length <10 )
            {
                return View("Cellphone numbers entered are less than 10 ");
            }
            if (passengerViewModel.Numbers.Length > 10)
            {
                return View("Cellphone numbers entered are more than 10 ");
            }

            var results = await _httpClient.PostAsJsonAsync(apiUrl, passengerViewModel);

            if (results.IsSuccessStatusCode)
            {
                //string sMessage = "Passenger Profile successfully created!";

                return RedirectToAction(nameof(PassengerIndex), new { Message = "Passenger Profile successfully created!" });
            }

            ModelState.AddModelError("", "Failed to create new passenger profile");
            return View(passengerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var passengerViewModel = new PassengerViewModel();
                return View(passengerViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public async Task<IActionResult> PassengerDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Passenger/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }
        [HttpGet]
        public IActionResult PassengerPut()
        {

            return View();
        }

        public async Task<IActionResult> PassengerPut(int? Id, PassengerViewModel Passengert)
        {
            var apiUrl = _options.ApiUrl + "/Passenger/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, Passengert);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(PassengerIndex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
            
        }
    }
}

using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class TimeslotController : BaseWebController
    {
        public TimeslotController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> Timeslotindex()
        {
           var apiUrl = _options.ApiUrl + "/Timeslot" ;


            var timeresponce = await _httpClient.GetStringAsync(apiUrl);

            var timelist = JsonConvert.DeserializeObject<IEnumerable<Timeslot>>(timeresponce);

            return View(timelist);
        }

        public async Task<IActionResult> TimeslotDetails(int? Id)

        {

            var apiUrl = _options.ApiUrl + "/Timeslot/" + Id;
            var timeslotres = await _httpClient.GetStringAsync(apiUrl);
            var timeslottodo = JsonConvert.DeserializeObject<Timeslot>(timeslotres);

            return View(timeslottodo);


        }
		public async Task<IActionResult> TimeslotDelete(int? Id)
		{
			var apiUrl = _options.ApiUrl + "/Timeslot/" + Id;
			// var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
			var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
			//  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
			return View();
		}

        [HttpGet]
        public IActionResult TimeslotPut()
        {

            return View();
        }

        public async Task<IActionResult> TimeslotPut(int? Id, TimeslotViewModel timeslott)
        {
            var apiUrl = _options.ApiUrl + "/Timeslot/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, timeslott);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(Timeslotindex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
            ;

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TimeslotViewModel timeslot)
        {

            var ApiUrl = _options.ApiUrl + "/Timeslot";


            var timeslotres = await _httpClient.PostAsJsonAsync(ApiUrl, timeslot);



            if (timeslotres.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return RedirectToAction(nameof(Timeslotindex));
              //  return Redirect(Url);


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter to enter to TimeSlot Delete");
            }
            return View();

        }

    }
}

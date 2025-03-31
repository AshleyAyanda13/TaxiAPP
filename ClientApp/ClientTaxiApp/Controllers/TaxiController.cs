using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientTaxiApp.Controllers
{
    public class TaxiController : BaseWebController
    {
        public TaxiController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }
        public async Task<IActionResult> TaxiIndex()
        {
            var apiUrl = _options.ApiUrl + "/Taxi";


            var taxiresponce = await _httpClient.GetStringAsync(apiUrl);

            var taxilist = JsonConvert.DeserializeObject<IEnumerable<Taxi>>(taxiresponce);

            return View(taxilist);
        }
        public async Task<IActionResult> TaxiDetails(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Taxi/" + Id;
            var taxires = await _httpClient.GetStringAsync(apiUrl);
            var taxitodo = JsonConvert.DeserializeObject<Taxi>(taxires);

            return View(taxitodo);
        }
        public async Task<IActionResult> TaxiDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Taxi/" + Id;
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
        public async Task<IActionResult> Create(TaxiPutModel taxicreate)
        {

            var ApiUrl = _options.ApiUrl + "/Taxi";


            var taxires = await _httpClient.PostAsJsonAsync(ApiUrl, taxicreate);



            if (taxires.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return Redirect(Url);


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter to enter to a new Taxi");
            }
            return View();

        }



        [HttpGet]
		public IActionResult TaxPut()
		{
			
			return View();
		}

		public async Task<IActionResult> TaxPut(int? Id, TaxiPutModel taxputt)
        {
            var apiUrl = _options.ApiUrl + "/Taxi/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, taxputt);


            if(marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(TaxiIndex));
            }
            else
            

                ModelState.AddModelError("","Failed to edit records!");
                return View();
          
           
        }

    }
}
 

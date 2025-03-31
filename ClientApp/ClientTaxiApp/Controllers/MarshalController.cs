using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ClientTaxiApp.Controllers
{
    public class MarshalController : BaseWebController
    {
        public MarshalController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = _options.ApiUrl + "/Marshal";


            var responce = await _httpClient.GetStringAsync(apiUrl);

            var marshallist = JsonConvert.DeserializeObject<IEnumerable<TaxiMarshal>>(responce);

            return View(marshallist);
        }
        public async Task<IActionResult> MarshalDetails(int? Id)

        {

            var apiUrl = _options.ApiUrl + "/Marshal/" + Id;
            var marshalres = await _httpClient.GetStringAsync(apiUrl);
            var marshaltodo = JsonConvert.DeserializeObject<TaxiMarshal>(marshalres);

            return View(marshaltodo);


        }
        public async Task<IActionResult> MarshalDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Marshal/" + Id;
           // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }

        [HttpGet]
        public IActionResult MarshalPut()
        {

            return View();
        }

        public async Task<IActionResult> MarshalPut(int? Id, MarshalViewModels marshalt)
        {
            var apiUrl = _options.ApiUrl + "/Marshal/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, marshalt);


            if (!marshalresponce.IsSuccessStatusCode)
            {

                ModelState.AddModelError("", "Failed to edit records!");
                return View();

            }
             

           return RedirectToAction(nameof(Index)); ;

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MarshalViewModels marshalcreate)
        {

            var ApiUrl = _options.ApiUrl + "/Marshal";


            var marshalres = await _httpClient.PostAsJsonAsync(ApiUrl, marshalcreate);



            if (marshalres.IsSuccessStatusCode)
            {

                var Url = _options.ApiUrl + "/Account/Login";
                return RedirectToAction(nameof(Index));
               // return Redirect(Url);


            }
            else
            {
                ModelState.AddModelError("", "Failed to enter to enter to a new marshal");
            }
            return View();

        }


        //private async Task<IActionResult> Delete()
        //{
        //    var apiUrl = _options.ApiUrl + "/Marshal/" + Id;

        //    var deleteResponse = await _httpClient.DeleteAsync(apiUrl);

        //    if (deleteResponse.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index), new { Message = "Marshal successfully deleted!" });
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }

        //}
    }
}

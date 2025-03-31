using ClientTaxiApp.Models;
using ClientTaxiApp.Models.ViewModels;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClientTaxiApp.Controllers
{
    public class PaymentsController : BaseWebController
    {
        public PaymentsController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public async Task<IActionResult> PaymentsIndex()
        {
            var apiUrl = _options.ApiUrl + "/Payments";


            var paymentsresponce = await _httpClient.GetStringAsync(apiUrl);

            var paymentslist = JsonConvert.DeserializeObject<IEnumerable<Payments>>(paymentsresponce);

            return View(paymentslist);
        }

        public async Task<IActionResult> PaymentDetails(int? Id)
        {

            var apiUrl = _options.ApiUrl + "/Payments/" + Id;
            var paymentres = await _httpClient.GetStringAsync(apiUrl);
            var paymenttodo = JsonConvert.DeserializeObject<Payments>(paymentres);

            return View(paymenttodo);
        }
        public async Task<IActionResult> PaymentsDelete(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Payments/" + Id;
            // var marshalresponce = await _httpClient.GetStringAsync(apiUrl);
            var deleteResponse = await _httpClient.DeleteAsync(apiUrl);
            //  var marshaldelete = JsonConvert.DeserializeObject<TaxiMarshal>(marshalresponce);
            return View();
        }

        [HttpGet]
        public IActionResult ProvincePut()
        {

            return View();
        }

        public async Task<IActionResult> PaymentPut(int? Id, ProvinceViewModel paymentt)
        {
            var apiUrl = _options.ApiUrl + "/Payments/" + Id;


            var marshalresponce = await _httpClient.PutAsJsonAsync(apiUrl, paymentt);


            if (marshalresponce.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(PaymentsIndex));
            }
            else
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
          

        }
        //process new payment  
        [HttpPost]
        public async Task<IActionResult> Create(PaymentsViewModel payViewModel)
        {

            var apiUrl = _options.ApiUrl + "/Payments";

           

            // Access additional form fields directly from FormCollection
            string streetAddress = Request.Form["StreetAddress"];
            string city = Request.Form["City"];
            string postalCode = Request.Form["PostalCode"];
            string amount = Request.Form["Amount"];

            payViewModel.BillingAdd = streetAddress + ", "+city + ", "+postalCode;
            payViewModel.Amount = decimal.Parse(amount);

            //verify payment status
            if (payViewModel.CardName != null && payViewModel.BillingAdd != null && payViewModel.Amount != null)
            {
                payViewModel.PaymentStatus = "Success";
            }

            
            payViewModel.PaymentDate = DateTime.Now;

            var results = await _httpClient.PostAsJsonAsync(apiUrl, payViewModel);            

            if (results.IsSuccessStatusCode)
            {

                return Redirect("/Bookings/BookingsIndex");
            }

            payViewModel.PaymentStatus = "Failed";

            ModelState.AddModelError("", "Payment Failed");
            return View(payViewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var payViewModel = new PaymentsViewModel();
                return View(payViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

    }
}

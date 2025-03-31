using ClientTaxiApp.Models;
using ClientTaxiApp.Models.ViewModels;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using TaxiAppClient.Models;

namespace ClientTaxiApp.Controllers
{
    public class BookingsController : BaseWebController
    {
      
        public BookingsController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }

        public IActionResult About() { return View(); }
        public IActionResult Homes() { return View(); }
        public async Task<IActionResult> BookingsIndex()
        {
            var apiUrl = _options.ApiUrl + "/Bookings";
            var bookingsresponce = await _httpClient.GetStringAsync(apiUrl);
            var bookingslist = JsonConvert.DeserializeObject<IEnumerable<Bookings>>(bookingsresponce);

            return View(bookingslist);
        }
        public async Task<IActionResult> BookingDetails(int? Id)
        {
            var apiUrl = _options.ApiUrl + "/Bookings/" + Id;
            var bookingres = await _httpClient.GetStringAsync(apiUrl);
            var bookingtodo = JsonConvert.DeserializeObject<Bookings>(bookingres);

            return View(bookingtodo);
        }

        //create new booking 
        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel bookingViewModel)
        {

            var apiUrl = _options.ApiUrl + "/Bookings";

            //fetch passenger data
            var passengerUrl = _options.ApiUrl + "/Passenger";
            var passRes = await _httpClient.GetStringAsync(passengerUrl);
            var passenger = JsonConvert.DeserializeObject<Passenger>(passRes);

            //fetch logged in user data
            var loggedUserUrl = _options.ApiUrl + "/Account/Login";
            var res = await _httpClient.GetStringAsync(loggedUserUrl);
            var user = JsonConvert.DeserializeObject<AppUser>(res);

            bookingViewModel.PassengerId = passenger.Id;

            //error handling for user without a profile
            if (passenger.AppUser.Id != user.Id && passenger.AppUser == null)
            {
                return Redirect("Passenger/Create");

            }

            //get taxi marshal
            var marshalUrl = _options.ApiUrl + "/Marshal";
            var marshalres = await _httpClient.GetStringAsync(marshalUrl);
            var marshal = JsonConvert.DeserializeObject<IEnumerable<TaxiMarshal>>(marshalres);

            foreach (var mar in marshal)
            {
                if (marshal.Count() != null)
                {
                    if (mar.Id != null)
                    {
                        bookingViewModel.MarshalId = mar.Id;
                    }
                }
            }

            bookingViewModel.DateCreated = DateTime.Now;

            var results = await _httpClient.PostAsJsonAsync(apiUrl, bookingViewModel);

            if (results.IsSuccessStatusCode)
            {
                    // Redirect to the payment page
                return Redirect("/Payments/Create");

            }

            ModelState.AddModelError("", "Failed to create new booking");
            PopulateDDL().Wait();
            return View(bookingViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var bookingViewModel = new BookingViewModel();
                await PopulateDDL();
                return View(bookingViewModel);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        //populate drop-down list
        private async Task PopulateDDL()
        {
            // Fetch cities
            var cityApiUrl = _options.ApiUrl + "/City";
            var cityRes = await _httpClient.GetStringAsync(cityApiUrl);
            var cities = JsonConvert.DeserializeObject<IEnumerable<Cities>>(cityRes);
            ViewBag.CityId = new SelectList(cities.ToList(), "Id", "Name");

            // Fetch timeslots
            var timeslotApiUrl = _options.ApiUrl + "/Timeslot";
            var timeslotRes = await _httpClient.GetStringAsync(timeslotApiUrl);
            var timeslots = JsonConvert.DeserializeObject<IEnumerable<Timeslot>>(timeslotRes);
            ViewBag.TimeslotId = new SelectList(timeslots.ToList(), "Id", "Slot");
        }
        public async Task<IActionResult> BookingDelete(int? Id)
        {
            var delApiUrl = _options.ApiUrl + "/Bookings/" + Id;
           
            var delRes = await _httpClient.DeleteAsync(delApiUrl);

            if (delRes.IsSuccessStatusCode)
            {

                return Redirect("/Bookings/BookingsIndex");
            }

            return View();
        }



        //Update Bookings
        [HttpGet]
        public IActionResult BookingPut()
        {

            return View();
        }

        public async Task<IActionResult> BookingPut(int? Id, BookingViewModel bookingViewModel)
        {
            var apiUrl = _options.ApiUrl + "/Booking/" + Id;


            var upRes = await _httpClient.PutAsJsonAsync(apiUrl, bookingViewModel);


            if (upRes.IsSuccessStatusCode)
            {

                return Redirect("/Bookings/BookingsIndex");
            }
          
            

                ModelState.AddModelError("", "Failed to edit records!");
                return View();
        }
    }
}

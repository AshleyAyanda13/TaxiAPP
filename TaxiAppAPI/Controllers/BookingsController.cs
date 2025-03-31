using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
  [Authorize]
    public class BookingsController : BaseController
    {
        //public BookingsController(AppDbContext appDbContext) : base(appDbContext)
        //{

        //}

        private readonly UserManager<AppUser> userManager;
        public BookingsController(AppDbContext appDbContext, UserManager<AppUser> userManager) : base(appDbContext)
        {

            this.userManager = userManager;

        }


        //Get All
      [Authorize(Roles = "AppUser")]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _appDbContext.Bookings.ToList();

            foreach (var booking in items)
            {
                _appDbContext.Entry(booking).Reference(b => b.Passenger).Load();
                _appDbContext.Entry(booking).Reference(b => b.City).Load();
                _appDbContext.Entry(booking.City).Reference(city => city.Province).Load();
                _appDbContext.Entry(booking).Reference(b => b.Timeslot).Load();
                _appDbContext.Entry(booking).Reference(b => b.Marshal).Load();
            }


            if(items==null)
            {

                return NotFound("Something went wrong"); 
            }
            else
            { 
                    return Ok(items);
                
            }


        }

        //Get by Id
        [Authorize(Roles = "AppUser, Admin")]

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
           var booking = await _appDbContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound("Something went wrong");
            }
            else
            {
         
                return Ok(booking);
            }
        }

        //Post-Create new booking
         [Authorize(Roles = "AppUser, Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(BookingsDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { bookingDTO, errorMsg = "Some of the values entered are invalid" });
            }

            var booking = bookingDTO.Bookings();
            booking.Passenger = _appDbContext.Passengers.Find(bookingDTO.PassengerId);
            booking.City = _appDbContext.Cities.Find(bookingDTO.CityId);
            booking.Timeslot = _appDbContext.Timeslot.Find(bookingDTO.TimeslotId);
            booking.Marshal = _appDbContext.TaxiMarshals.Find(bookingDTO.MarshalId);


            await _appDbContext.Bookings.AddAsync(booking);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { booking, msg = $"New booking was successfully created!" });
            }

            return BadRequest("Failed to create new booking");

        }

        //Put- Update Bookings
        [Authorize(Roles = "AppUser, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, BookingsDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { bookingDTO, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var booking = await _appDbContext.Bookings.FindAsync(id);
            var passenger = await _appDbContext.Passengers.FindAsync(bookingDTO.PassengerId);
            var city = await _appDbContext.Cities.FindAsync(bookingDTO.CityId);
            var time = await _appDbContext.Timeslot.FindAsync(bookingDTO.TimeslotId);
            var marshal = await _appDbContext.TaxiMarshals.FindAsync(bookingDTO.MarshalId);

            if (booking == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            booking.BookingDate = bookingDTO.BookingDate;
            booking.Passenger = passenger;
            booking.City = city;
            booking.Timeslot = time;
            booking.Marshal = marshal;

            //update bookings object
            _appDbContext.Update(booking);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {id}");

        }

        //Delete Booking record
         [Authorize(Roles = "Admin, AppUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            //Find Record
            if (id == null)
            {
                return BadRequest("The id value is required!");
            }

            var booking = await _appDbContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete bookings object
            _appDbContext.Remove(booking);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }

    }
}

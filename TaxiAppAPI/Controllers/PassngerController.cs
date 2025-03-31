using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
   // [Authorize]
    public class PassengerController : BaseController
    {
       
        public PassengerController(AppDbContext appDbContext, UserManager<AppUser> userManager) : base(appDbContext,userManager)
        {

            

        }


        //Get All
       // [Authorize(Roles = "AppUser, Admin")]
        [HttpGet]
        public IActionResult Get()
        {

            var users = _userManager.Users.FirstOrDefault(s => s.UserName == User.Identity.Name);
          
            var item = _appDbContext.Passengers.Include(appuser => appuser.AppUser).ToList();











            if (item == null||User==null)
            {

                return NotFound("Something Went Wrong!");
            }
            else
            {



                var items = item.FirstOrDefault(appuser => appuser.AppUser.UserName == users.UserName);


                

                
                

                return Ok(items);
           }
        }
        //Get by Id
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]

        public async Task<IActionResult> Get(int? Id)
        {
            if (Id == 0)
                return BadRequest($"The id {Id} value is required");
            Passenger passengers = await _appDbContext.Passengers.FindAsync(Id);

            if (passengers == null)
            {
                return NotFound("No Bookings Found!");
            }
            else
            {
                return Ok(passengers);
            }

        }
        //Post-Create new Profile
        [Authorize(Roles = "AppUser, Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(PassengerDTO passengersDTO)
        {
           
            if (!ModelState.IsValid)
            {

               return BadRequest(new { passengersDTO, errorMsg = "Some of your values are invalid" });
            }


            var passenger = passengersDTO.Passengers();
            passenger.AppUser = await _userManager.FindByIdAsync(passengersDTO.appUserId);

            await _appDbContext.Passengers.AddAsync(passenger);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { passenger, msg = $"New record was successfully created!" });
                
                
            }

            return BadRequest("Failed to create new record");
        }



        //Put- Update Passenger
        [Authorize(Roles = "AppUser, Admin")]
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int? Id, PassengerDTO passengerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { passengerDTO, errorMsg = "Some of your values are invalid" });
            }

            if (Id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var passengers = await _appDbContext.Passengers.FindAsync(Id);

            if (passengers == null)
            {
                return BadRequest($"The 'id' {Id} entered does not match existing records!");
            }
            //update to DTO fields

            passengers.PassengerName = passengerDTO.Name;
            passengers.Surname = passengerDTO.Surname;
            passengers.CellphoneNr = passengerDTO.Numbers;

            //update Passenger object
            _appDbContext.Passengers.Update(passengers);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {Id}");
        }
        //Delete Citys record
        [Authorize(Roles = "AppUser, Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int? Id)
        {

            //Find Record
            if (Id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var passengers = await _appDbContext.Passengers.FindAsync(Id);

            if (passengers == null)
            {
                return BadRequest($"The 'id' {Id} entered does not match existing records!");
            }

            //delete bookings object
            _appDbContext.Passengers.Remove(passengers);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {Id}");

        }
    }
}

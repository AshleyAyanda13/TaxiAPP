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
    public class TimeslotController : BaseController
    {
        //public TimeslotController(AppDbContext appDbContext) : base(appDbContext)
        //{
        //}
        private readonly UserManager<AppUser> userManager;

        public TimeslotController(AppDbContext appDbContext, UserManager<AppUser> userManager) : base(appDbContext)
        {



            this.userManager = userManager;



        }


        //Get All Timeslot Records depending on user
        [Authorize(Roles = "AppUser, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _appDbContext.Timeslot.ToList();

            if (items == null)
            {

                return NotFound("Something Went Wrong!");
            }
            else
            {
                    return Ok(items);
           }
        }

        //Get by Id by only the admin
        [Authorize(Roles = "AppUser, Admin")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
            Timeslot timeslot = await _appDbContext.Timeslot.FindAsync(id);

            if (timeslot == null)
            {
                return NotFound("Somthing Went Wrong!");
            }
            else
            {
                return Ok(timeslot);
            }

        }

        //Post-Create new timeslot record //only by Admin
        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(TimeslotDTO timeslot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { timeslot, errorMsg = "Some of your values are invalid" });
            }


            await _appDbContext.Timeslot.AddAsync(timeslot.Timeslot());

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { timeslot, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update Bookings//only by Admin
        [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, TimeslotDTO timeslotDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { timeslotDTO, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var timeslot = await _appDbContext.Timeslot.FindAsync(id);

            if (timeslot == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            timeslot.Slot = timeslotDTO.Slot;
           
            //update bookings object
            _appDbContext.Timeslot.Update(timeslot);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {id}");

        }

        //Delete Booking record//Only by Admin
        [Authorize(Roles = " Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            //Find Record
            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var timeslot = await _appDbContext.Timeslot.FindAsync(id);

            if (timeslot == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete bookings object
            _appDbContext.Timeslot.Remove(timeslot);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }
    }
}

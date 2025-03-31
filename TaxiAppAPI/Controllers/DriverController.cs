using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    [Authorize]
    public class DriverController : BaseController
    {
        public DriverController(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        //Get All
        [Authorize(Roles = " Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var Drivers = _appDbContext.Driver.ToList();


            if (Drivers == null)
            {
                 
                return NotFound("Something Went Wrong");
            }
            else
            {
              return Ok(Drivers);
                
            }
        }

        //Get by Id
        [Authorize(Roles = " Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
            Driver driver = await _appDbContext.Driver.FindAsync(id);

            if (driver == null)
            {
                return NotFound("Something Went Wrong!");
            }

            return Ok(driver);
        }


        //Post-Create new Taxi Record
        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(DriverDTO driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { driver, errorMsg = "Some of your values are invalid" });
            }


            await _appDbContext.Driver.AddAsync(driver.ToDriver());

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { driver, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update The Taxi Table
        [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, DriverDTO driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { driver, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            Driver driverr = await _appDbContext.Driver.FindAsync(id);

            if (driverr == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            driverr.DriverName = driver.DriverName;
            driverr.DriverSurname = driver.DriverSurname;
           

            //update taxi object
            _appDbContext.Update(driverr);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {id}");

        }

        //Delete Taxi record
        [Authorize(Roles = " Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            //Find Record
            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var driver = await _appDbContext.Driver.FindAsync(id);

            if (driver == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete Taxis object
            _appDbContext.Remove(driver);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }




































    }
}

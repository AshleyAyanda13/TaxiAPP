using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{

    public class DriverDelController : BaseController
    {

         public DriverDelController(AppDbContext appDbContext) : base(appDbContext)
         {}


        //Get All
     //   [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var driverdel = _appDbContext.DriverDel.ToList();

            foreach (var driver in driverdel)
            {
                _appDbContext.Entry(driver).Reference(d => d.Driver).Load();

            }


            if (driverdel == null)
            {
                return NotFound("Something Went Wrong"); 
            }

            else
            {
                return Ok(driverdel);

            }
        }

        //Get by Id
       // [Authorize(Roles = " Admin")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
            DriverDel driverdelete = await _appDbContext.DriverDel.FindAsync(id);

                _appDbContext.Entry(driverdelete).Reference(d => d.Driver).Load();
            if (driverdelete == null)
            {
                return NotFound("Something went Wrong");
            }
            return Ok(driverdelete);
        }


        //Post-Create new Driver Deleted Record
        //[Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(DriverDelDTO driverDelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { driverDelDTO, errorMsg = "Some of your values are invalid" });
            }

            var driver = driverDelDTO.ToDriverDel();
            driver.Driver = _appDbContext.Driver.Find(driverDelDTO.DriverId);
    


            await _appDbContext.DriverDel.AddAsync(driver);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { driverDelDTO, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update The Taxi Table
     //   [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, DriverDelDTO drivedel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { drivedel, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var driverDel = await _appDbContext.DriverDel.FindAsync(id);
            var driver = await _appDbContext.Driver.FindAsync(drivedel.DriverId);

            if (driverDel == null||driver==null)
            {
                return BadRequest($"Something Went Wrong");
            }



            //update to DTO fields
            driverDel.DeleteDate = drivedel.DeleteDate;
            driverDel.Driver = driver;


            //update taxi object
            _appDbContext.Update(driverDel);

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

            var driverDel = await _appDbContext.DriverDel.FindAsync(id);

            if (driverDel == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete Taxis object
            _appDbContext.Remove(driverDel);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }


















    }
}

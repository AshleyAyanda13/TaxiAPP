using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    [Authorize]
    public class TaxiController : BaseController
    {
        public TaxiController(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        //Get All
        [Authorize(Roles = " Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var taxis = _appDbContext.Taxi.ToList();


            foreach (var taxi in taxis)
            {
                _appDbContext.Entry(taxi).Reference(t => t.Driver).Load();

            }



            if (taxis == null)
            {

                return NotFound("Something went Wrong");

            }
            else
            {
             return Ok(taxis);
                 
            }
        }

        //Get by Id
        [Authorize(Roles = " Admin")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
            Taxi taxi = await _appDbContext.Taxi.FindAsync(id);

            if (taxi == null)
            {
                return NotFound("Something went Wrong!");
            }
            else
            {
                return Ok(taxi);
            }
        }


        //Post-Create new Taxi Record
        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(TaxiDTO taxiDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { taxiDTO, errorMsg = "Some of your values are invalid" });
            }

            var taxi = taxiDTO.Taxis();
            taxi.Driver = _appDbContext.Driver.Find(taxiDTO.DriverId);

            await _appDbContext.Taxi.AddAsync(taxi);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { taxi, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update The Taxi Table
        [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, TaxiDTO taxi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { taxi, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var taxxi = await _appDbContext.Taxi.FindAsync(id);
            var driver = _appDbContext.Driver.Find(id);
            if (taxxi == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            taxxi.BrandName = taxi.LicensePlate;
            taxxi.Type = taxi.Type;
            taxxi.LicensePlate = taxi.LicensePlate;
            taxxi.Driver = driver;
            taxxi.Seats = taxi.Seats;

            //update taxi object
            _appDbContext.Update(taxxi);

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

            var taxi = await _appDbContext.Taxi.FindAsync(id);

            if (taxi == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete Taxis object
            _appDbContext.Remove(taxi);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }

    }
}

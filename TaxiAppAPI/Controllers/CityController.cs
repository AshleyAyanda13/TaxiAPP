using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        public CityController(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        //Get All
        [Authorize(Roles = "AppUser, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
                var items = _appDbContext.Cities
                .ToList();

            foreach (var city in items)
            {
                _appDbContext.Entry(city).Reference(c => c.Taxi).Load();
                _appDbContext.Entry(city.Taxi).Reference(taxi => taxi.Driver).Load();
                _appDbContext.Entry(city).Reference(c => c.Province).Load();
            }

            //var cities = _appDbContext.Cities
            //.Include(c => c.Province)
            //.Include(c => c.Taxi)
            //.ToList();




            if (items == null)
            {


                return NotFound("Something Went wrong!");

            }
            else
            { 
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
            Cities citys = await _appDbContext.Cities.FindAsync(Id);

            if (citys == null)
            {
                return NotFound("Something went Wrong!");
            }

            return Ok(citys);

        }
        //Post-Create new City
        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(CityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { cityDTO, errorMsg = "Some of your values are invalid" });
            }

            var city = cityDTO.Cities();
            city.Province = _appDbContext.Province.Find(cityDTO.ProvinceId);
            city.Taxi = _appDbContext.Taxi.Find(cityDTO.TaxiId);

            await _appDbContext.Cities.AddAsync(city);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { cityDTO, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");
        }
        //Put- Update City
        [Authorize(Roles = " Admin")]
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int? Id, CityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { cityDTO, errorMsg = "Some of your values are invalid" });
            }

            if (Id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var citys = await _appDbContext.Cities.FindAsync(Id);
            var province = await _appDbContext.Province.FindAsync(Id);



            if (citys == null)
            {
                return BadRequest($"The 'id' {Id} entered does not match existing records!");
            }
            //update to DTO fields
            citys.Name = cityDTO.Name;
            citys.Province = province;

            //update City object
            _appDbContext.Cities.Update(citys);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {Id}");
        }
        //Delete Citys record
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int? Id)
        {

            //Find Record
            if (Id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var citys = await _appDbContext.Cities.FindAsync(Id);

            if (citys == null)
            {
                return BadRequest($"The 'id' {Id} entered does not match existing records!");
            }

            //delete bookings object
            _appDbContext.Cities.Remove(citys);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {Id}");

        }

    }
}

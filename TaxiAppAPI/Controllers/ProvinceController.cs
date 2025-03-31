using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    [Authorize]
    public class ProvinceController : BaseController
    {
        public ProvinceController(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        //Get All Province Records
        [Authorize(Roles = "AppUser, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _appDbContext.Province.ToList();

            if (items == null)
            {

                return NotFound("Something went Wrong");

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
            Province province = await _appDbContext.Province.FindAsync(id);

            if (province == null)
            {
                return NotFound("Something Went Wrong");
            }
            else
            {
                return Ok(province);
            }
        }

        //Post-Create new province record
        [Authorize(Roles = " Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ProvinceDTO province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { province, errorMsg = "Some of your values are invalid" });
            }


            await _appDbContext.Province.AddAsync(province.Province());

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { province, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update Province
        [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, ProvinceDTO provinceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { provinceDTO, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var province = await _appDbContext.Province.FindAsync(id);

            if (province == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            province.Name = provinceDTO.Name;

            //update province object
            _appDbContext.Province.Update(province);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {id}");

        }

        //Delete Province record
        [Authorize(Roles = " Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            //Find Record
            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var province = await _appDbContext.Province.FindAsync(id);

            if (province == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete province object
            _appDbContext.Province.Remove(province);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }
    }
}

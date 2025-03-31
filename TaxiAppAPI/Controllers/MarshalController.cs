using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    //[Authorize]
    public class MarshalController : BaseController
    {
        public MarshalController(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        //Get All 
      //  [Authorize(Roles = " Admin, AppUser")]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _appDbContext.TaxiMarshals.ToList();
            if (items == null)
            {
                return BadRequest("The item isnt found");
            }
            
            else
            { 
                return Ok(items);
           }


        }

        //Get Marshal by Id
      //  [Authorize(Roles = " Admin, AppUser")]
        [HttpGet("{Id}")]

        public async Task<IActionResult> Get(int? Id)
        {
            if (Id == 0)
            {
                return BadRequest($"The id {Id}value is required");
            }

            TaxiMarshal taxiMarshal = await _appDbContext.TaxiMarshals.FindAsync(Id);

            if (taxiMarshal == null)
            {
                return NotFound("Something Went Wrong");
            }
            else
            {
                return Ok(taxiMarshal);
            }
        }

        //Get Marshal By  Post
      //  [Authorize(Roles = " Admin")]
        [HttpPost]

        public async Task<IActionResult> Post(MarshalDTO taxiMarshal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { taxiMarshal, ErrMsg = "Some of you texts are invalid" });
            }
            await _appDbContext.TaxiMarshals.AddAsync(taxiMarshal.TaxiMarshals());

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { taxiMarshal, ErrMsg = "everything is good" });
            }

            return BadRequest("Nothing is working");
        }
        //Get Marshal BY PUT ID
     //   [Authorize(Roles = " Admin")]
        [HttpPut("{Id}")]

        public async Task<IActionResult> Put(int? Id, MarshalDTO marshalDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { marshalDTO, errmsg = "some values are missing" });
            }

            if (Id == null)
            {
                return BadRequest("please try again");
            }

            var taxiMarshal = await _appDbContext.TaxiMarshals.FindAsync(Id);
            if (taxiMarshal == null)
            {
                return BadRequest($"The id {Id}value is required");
            }

            //updating the DTOs

            taxiMarshal.MarshalName = marshalDTO.MarshalName;
            taxiMarshal.MarshalSurname = marshalDTO.MarshalSurname;

            //update the object 
            _appDbContext.TaxiMarshals.Update(taxiMarshal);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"The id {Id}value is required");

        }
        //Get Delete by Id
      //  [Authorize(Roles = " Admin")]
        [HttpDelete("{Id}")]

        public async Task<IActionResult> Delete(int? Id)
        {
            //Find Record
            if (Id == null)
            {
                return BadRequest("Bad Request");
            }

            var taxiMarshal = await _appDbContext.TaxiMarshals.FindAsync(Id);

            if (taxiMarshal == null)
            {
                return BadRequest($"The id {Id}value is required");
            }

            //delete bookings object
            _appDbContext.TaxiMarshals.Remove(taxiMarshal);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"The id {Id}value is required");
        }
    }
}

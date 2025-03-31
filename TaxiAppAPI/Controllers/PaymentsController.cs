using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;
using TaxiAppApi.Data;
using TaxiAppApi.DTOs;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    public class PaymentsController : BaseController
    {
      
        public PaymentsController(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        //Get All Payments Records
        [Authorize(Roles = " Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var items = _appDbContext.Payments.ToList();

            //foreach (var payment in items)
            //{
            //    _appDbContext.Entry(payment).Reference(p => p.Booking).Load();
            //    _appDbContext.Entry(payment.Booking).Reference(passenger => passenger.Passenger).Load();
            //    _appDbContext.Entry(payment.Booking).Reference(city => city.City).Load();
            //    _appDbContext.Entry(payment.Booking).Reference(time => time.Timeslot).Load();
            //    _appDbContext.Entry(payment.Booking).Reference(Marshal => Marshal.Marshal).Load();
                
            //}


            if (items == null)
            {
                return NotFound("Something Went Wrong");

            }


            else
            {
                return Ok(items);
           }
        }

        //Get by Id
        [Authorize(Roles = " Admin")]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int? id)
        {
            if (id == 0)
                return BadRequest($"The id {id} value is required");
            Payments payment = await _appDbContext.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound("No Payment is Found!");
            }

            return Ok(payment);

        }

        //Post-Create new payment record
        [Authorize(Roles = "AppUser, Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(PaymentsDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { paymentDTO, errorMsg = "Some of your values are invalid" });
            }

            var payment = paymentDTO.Payments();
            //payment.Booking = _appDbContext.Bookings.Find(paymentDTO.BookingId);


            await _appDbContext.Payments.AddAsync(payment);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return CreatedAtAction(nameof(Post), new { payment, msg = $"New record was successfully created!" });
            }

            return BadRequest("Failed to create new record");

        }

        //Put- Update Bookings
        [Authorize(Roles = " Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, PaymentsDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { paymentDTO, errorMsg = "Some of your values are invalid" });
            }

            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var payment = await _appDbContext.Payments.FindAsync(id);
            var booking = await _appDbContext.Bookings.FindAsync(id);



            if (payment == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //update to DTO fields
            payment.Amount = paymentDTO.Amount;
            payment.PaymentDate = paymentDTO.PaymentDate;
            payment.PaymentStatus = paymentDTO.PaymentStatus;
            //payment.Booking = booking;

            //update bookings object
            _appDbContext.Update(payment);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to update record at id {id}");

        }

        //Delete Booking record
        [Authorize(Roles = " Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            //Find Record
            if (id == null)
            {
                return BadRequest("The id value does is required!");
            }

            var payment = await _appDbContext.Payments.FindAsync(id);

            if (payment == null)
            {
                return BadRequest($"The 'id' {id} entered does not match existing records!");
            }

            //delete bookings object
            _appDbContext.Remove(payment);

            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return NoContent();
            }
            return BadRequest($"Failed to delete record at id {id}");

        }
    }
}

using InsuranceAPIApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public PaymentController(AngularDbContext context)
        {
            _context = context;
        }

        [HttpGet("byuserid/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetPaymentsByUserId(int userId)
        {
            var payments = await _context.PaymentDetails
                .Where(p => p.UserId == userId)
                .Select(p => new
                {
                    p.Id,
                    p.CardOwnerName,
                    p.CardNumber,
                    p.SecurityCode,
                    p.ValidThrough,
                    p.UserId
                })
                .ToListAsync();

            if (payments == null || payments.Count == 0)
            {
                return NotFound($"No payments found for UserId {userId}.");
            }

            return payments;
        }

        [HttpGet("bypaymentid/{paymentId}")]
        public async Task<ActionResult<object>> GetPaymentDetailsById(int paymentId)
        {
            var payment = await _context.PaymentDetails
                .Where(p => p.Id == paymentId)
                .Select(p => new
                {
                    p.Id,
                    p.CardOwnerName,
                    p.CardNumber,
                    p.SecurityCode,
                    p.ValidThrough,
                    p.UserId
                })
                .FirstOrDefaultAsync();

            if (payment == null)
            {
                return NotFound($"Payment with ID {paymentId} not found.");
            }

            return payment;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddPayment([FromBody] PaymentInputModel paymentInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(false);
            }

            var paymentDetail = new PaymentDetail
            {
                CardOwnerName = paymentInput.CardOwnerName,
                CardNumber = paymentInput.CardNumber,
                SecurityCode = paymentInput.SecurityCode,
                ValidThrough = paymentInput.ValidThrough,
                UserId = paymentInput.UserId
            };

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpDelete("{paymentId}")]
        public async Task<ActionResult<bool>> DeletePayment(int paymentId)
        {
            var payment = await _context.PaymentDetails.FindAsync(paymentId);

            if (payment == null)
            {
                return NotFound($"Payment with ID {paymentId} not found.");
            }

            _context.PaymentDetails.Remove(payment);
            await _context.SaveChangesAsync();

            // Check if the payment still exists after deletion
            var isDeleted = await _context.PaymentDetails.FindAsync(paymentId) == null;

            return Ok(isDeleted);
        }
        [HttpPut("{paymentId}")]
        public async Task<ActionResult<bool>> UpdatePayment(int paymentId, [FromBody] PaymentInputModel paymentInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payment data.");
            }

            var payment = await _context.PaymentDetails.FindAsync(paymentId);

            if (payment == null)
            {
                return NotFound($"Payment with ID {paymentId} not found.");
            }

            // Update payment details with new input
            payment.CardOwnerName = paymentInput.CardOwnerName;
            payment.CardNumber = paymentInput.CardNumber;
            payment.SecurityCode = paymentInput.SecurityCode;
            payment.ValidThrough = paymentInput.ValidThrough;

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        public class PaymentInputModel
        {
            public string CardOwnerName { get; set; }
            public string CardNumber { get; set; }
            public string SecurityCode { get; set; }
            public DateTime ValidThrough { get; set; }
            public int UserId { get; set; }
        }


    }
}

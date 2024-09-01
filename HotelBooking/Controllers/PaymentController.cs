using HotelBooking.BusinessInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _PaymentService;
        public PaymentController(IPaymentService _paymentService)
        {
            _PaymentService = _paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetAllPayment()
        {
            var payment = await _PaymentService.GetAllPayment(); //we are storing data in variable
            return Ok(payment);
        }

        [HttpGet("{PaymentId}")]
        public async Task<ActionResult<PaymentModel>> GetPaymentById(int PaymentId)
        {
            var payment = await _PaymentService.GetPaymentById(PaymentId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost("initiate")] //start
        public async Task<IActionResult> InitiatePayment(PaymentRequestInitiateModel paymentRequest)
        {
            if (!ModelState.IsValid) //modelstate is not valid
            {
                return BadRequest(ModelState);
            }
            var response = await _PaymentService.InitiatePayment(paymentRequest);
            return Ok(response);
        }



        [HttpPost("Confirm")]
        public async Task<ActionResult> ConfirmPaymentRequest(ConfirmPaymentRequest confirmPaymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _PaymentService.ConfirmPayment(confirmPaymentRequest);
            return Ok(response);
        }

        [HttpDelete("{PaymentId}")]
        public async Task<ActionResult> DeletePayment(int PaymentId)
        {
            await _PaymentService.DeletePayment(PaymentId);
            return NoContent();
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment(RefundPaymentModel refundPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _PaymentService.RefundPayment(refundPayment);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, "An error occurred while processing the refund.");
            }

        }
    }


}

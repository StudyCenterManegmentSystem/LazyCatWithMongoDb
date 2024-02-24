using Application.Dtos.PaymentDtos;

namespace Web.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("create-payment")]  
    public async Task<IActionResult> AddPayment(AddPaymentDto dto)
    {
        try
        {
            var payment = await _paymentService.AddPaymentAsync(dto);
            return Ok(payment);
        }
        catch(ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update-payment")]
    public async Task<IActionResult> UpdatePayment(UpdatePaymentDto dto)
    {
        try
        {
            var payment = await _paymentService.UpdatePaymentAsync(dto);
            return Ok(payment);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

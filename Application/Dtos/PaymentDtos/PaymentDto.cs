using Application.Dtos.StudentDtos;
using Domain.Entities.Entity.Payments;

namespace Application.Dtos.PaymentDtos;

public class PaymentDto : BaseDto
{
    public DateTime QachonTolagan { get; set; }
    [Required]
    public decimal QanchaTolagan { get; set; }

    public string? paymentType { get; set; }

    public StudentForPayment? Student { get; set; }

}

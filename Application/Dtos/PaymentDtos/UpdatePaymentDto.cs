using Domain.Entities.Entity.Payments;

namespace Application.Dtos.PaymentDtos;

public class UpdatePaymentDto : BaseDto
{
    public string? StudentId { get; set; }
    public string? GroupId { get; set; }
    public DateTime QachonTolagan { get; set; }
    [Required]
    public decimal QanchaTolagan { get; set; }

    public PaymentType paymentType { get; set; }


    public static implicit operator Payment(UpdatePaymentDto dto)
    {
        return new Payment
        {
            Id = dto.Id,
            GroupId = dto.GroupId,
            StudentId = dto.StudentId,
            QachonTolagan = dto.QachonTolagan,
            QanchaTolagan = dto.QanchaTolagan,
            paymentType = dto.paymentType
        };
    }
    public static implicit operator UpdatePaymentDto(Payment dto)
    {
        return new UpdatePaymentDto
        {
            Id = dto.Id,
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            QanchaTolagan = dto.QanchaTolagan,
            QachonTolagan = dto.QachonTolagan,
            paymentType = dto.paymentType
        };
    }
}


using Application.Dtos.GroupsDto;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;

namespace Application.Dtos.PaymentDtos;

public class AddPaymentDto
{

    public string? StudentId { get; set; }
    public string? GroupId { get; set; }
    public DateTime QachonTolagan { get; set; }
    [Required]
    public decimal QanchaTolagan { get; set; }

    public PaymentType paymentType { get; set; }


    public static implicit operator Payment(AddPaymentDto dto)
    {
        return new Payment
        {
            GroupId = dto.GroupId,
            StudentId = dto.StudentId,
            QachonTolagan = dto.QachonTolagan,
            QanchaTolagan = dto.QanchaTolagan,
            paymentType = dto.paymentType
        };
    }
    public static implicit operator AddPaymentDto(Payment dto)
    {
        return new AddPaymentDto
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            QanchaTolagan = dto.QanchaTolagan,
            QachonTolagan = dto.QachonTolagan,
            paymentType = dto.paymentType
        };
    }
}

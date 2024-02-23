
namespace Domain.Entities.Entity.Payments;

public class Payment : BaseEntity
{
    public string? StudentId { get; set; }
    public string? GroupId  { get; set; }
    public DateTime QachonTolagan { get; set; }
    [Required]
    public decimal QanchaTolagan { get; set; }

    public PaymentType paymentType { get; set; }
}

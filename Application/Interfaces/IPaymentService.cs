using Application.Dtos.PaymentDtos;
using Domain.Entities.Entity.Payments;

namespace Application.Interfaces;

public interface IPaymentService
{
    Task<Payment> AddPaymentAsync(AddPaymentDto dto);
    Task<Payment> UpdatePaymentAsync(UpdatePaymentDto dto);
    Task<Payment> DeletePaymentAsync(string id);
    Task<List<PaymentDto>> GetAllPayments();
    Task<PaymentDto> GetByIdAsync(string id);

}

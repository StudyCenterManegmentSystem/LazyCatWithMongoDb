using Application.Dtos.PaymentDtos;
using Domain.Entities.Entity.Payments;

namespace Application.Services;

public class PaymentService(IUnitOfWork unitOfWork) : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Payment> AddPaymentAsync(AddPaymentDto dto)
    {
        if(dto == null)
        {
            throw new ArgumentNullException("Payment null!!");
        }
        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Payment identifikatorlari ObjectId ko'rinishida emas");
        }
        var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(dto.GroupId!);
        if(guruh == null)
        {
            throw new CustomException("Bunday guruh mavjud emas");
        }
        if (!ObjectId.TryParse(dto.StudentId, out ObjectId id))
        {
            throw new CustomException("Talaba identifikatorlari ObjectId ko'rinishida emas");
        }
        var student = await _unitOfWork.StudentInterface.GetByIdAsync(dto.StudentId!);
        if(student == null)
        {
            throw new CustomException("Bunday talaba mavjud emas");

        }
        var payment = (Payment)dto;
        if (!payment.IsValid())
        {
            throw new CustomException("Payment is invalid");
        }
        var allpayment = await _unitOfWork.PaymentInterface.GetAllAsync();
        if(payment.IsExist(allpayment))
        {
            throw new CustomException("Bunday to'lov oldin mavjud");
        }
        await _unitOfWork.PaymentInterface.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> DeletePaymentAsync(string id)
    {
        if(!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Payment id identifikatorlari ObjectId ko'rinishida emas");
        }
        var payment = await _unitOfWork.PaymentInterface.GetByIdAsync(id);
        if(payment == null)
        {
            throw new NotFoundException("Bunday payment topilmadi");
        }
        await _unitOfWork.PaymentInterface.DeleteAsync(id);
        return payment;
    }

    public async Task<Payment> UpdatePaymentAsync(UpdatePaymentDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException("Payment null!!");
        }
        if (!ObjectId.TryParse(dto.GroupId, out ObjectId objectId))
        {
            throw new CustomException("Payment identifikatorlari ObjectId ko'rinishida emas");
        }
        var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(dto.GroupId!);
        if (guruh == null)
        {
            throw new CustomException("Bunday guruh mavjud emas");
        }
        if (!ObjectId.TryParse(dto.StudentId, out ObjectId id))
        {
            throw new CustomException("Talaba identifikatorlari ObjectId ko'rinishida emas");
        }
        var student = await _unitOfWork.StudentInterface.GetByIdAsync(dto.StudentId!);
        if (student == null)
        {
            throw new CustomException("Bunday talaba mavjud emas");

        }
        var payment = (Payment)dto;
        if (!payment.IsValid())
        {
            throw new CustomException("Payment is invalid");
        }
       
        await _unitOfWork.PaymentInterface.UpdateAsync(payment);
        return payment;
    }
}

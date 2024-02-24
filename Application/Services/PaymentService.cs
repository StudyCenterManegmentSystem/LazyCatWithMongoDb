using Application.Dtos.GroupsDto;
using Application.Dtos.PaymentDtos;
using Application.Dtos.StudentDtos;
using Domain.Entities.Entity.Payments;

namespace Application.Services;

public class PaymentService(IUnitOfWork unitOfWork,
                            UserManager<Teacher> userManager) : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<Teacher> _userManager = userManager;

    public async Task<Payment> AddPaymentAsync(AddPaymentDto dto)
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
        var allpayment = await _unitOfWork.PaymentInterface.GetAllAsync();
        if (payment.IsExist(allpayment))
        {
            throw new CustomException("Bunday to'lov oldin mavjud");
        }
        await _unitOfWork.PaymentInterface.AddAsync(payment);
        return payment;
    }

    public async Task<Payment> DeletePaymentAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Payment id identifikatorlari ObjectId ko'rinishida emas");
        }
        var payment = await _unitOfWork.PaymentInterface.GetByIdAsync(id);
        if (payment == null)
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


    public async Task<List<PaymentDto>> GetAllPayments()
    {
        var payments = await _unitOfWork.PaymentInterface.GetAllAsync();
        List<PaymentDto> result = new List<PaymentDto>();

        foreach (var payment in payments)
        {
            if (!ObjectId.TryParse(payment.GroupId, out ObjectId guruhId))
            {
                throw new CustomException("GuruhId id identifikatorlari ObjectId ko'rinishida emas");
            }
            var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(payment.GroupId!);
            if (!ObjectId.TryParse(payment.StudentId, out ObjectId studentId))
            {
                throw new CustomException("Student id identifikatorlari ObjectId ko'rinishida emas");
            }
            var student = await _unitOfWork.StudentInterface.GetByIdAsync(payment.StudentId!);
            if (payment != null && student != null && guruh != null)
            {
                var paymentDto = new PaymentDto()
                {
                    Id = payment.Id,
                    QachonTolagan = payment.QachonTolagan,
                    QanchaTolagan = payment.QanchaTolagan,
                    paymentType = payment.paymentType.ToString(),
                    Student = new StudentForPayment()
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        PhoneNumber = student.PhoneNumber,
                        Guruh = new GuruhReturnDto
                        {
                            Id = guruh.Id,
                            GroupName = guruh.GroupName,
                            Weekdays = guruh.Weekdays,
                            Start = guruh.Start,
                            End = guruh.End,
                            Price = guruh.Price,
                            Duration = guruh.Duration,
                            Room = await _unitOfWork.RoomInterface.GetByIdAsync(guruh.RoomId!),
                            Teacher = await _userManager.FindByIdAsync(guruh.TeacherId!),
                            Fan = await _unitOfWork.FanRepository.GetByIdAsync(guruh?.FanId!),
                        }
                    }
                };

                result.Add(paymentDto);
            }
        }

        return result;
    }

    public async Task<PaymentDto> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            throw new CustomException("Payment id identifikatorlari ObjectId ko'rinishida emas");
        }
        var payment = await _unitOfWork.PaymentInterface.GetByIdAsync(id);
        if (payment == null)
        {
            throw new NotFoundException("Sizda hech qanday to'lovlar mavjud emas");
        }
        var result = new PaymentDto();

        if (payment != null)
        {
            if (!ObjectId.TryParse(payment.GroupId, out ObjectId guruhId))
            {
                throw new CustomException("GuruhId id identifikatorlari ObjectId ko'rinishida emas");
            }
            var guruh = await _unitOfWork.GuruhInterface.GetByIdAsync(payment.GroupId!);
            if (!ObjectId.TryParse(payment.StudentId, out ObjectId studentId))
            {
                throw new CustomException("Student id identifikatorlari ObjectId ko'rinishida emas");
            }
            var student = await _unitOfWork.StudentInterface.GetByIdAsync(payment.StudentId!);
         

            if (student != null && guruh != null)
            {
                result = new PaymentDto()
                {
                    Id = payment.Id,
                    QachonTolagan = payment.QachonTolagan,
                    QanchaTolagan = payment.QanchaTolagan,
                    paymentType = payment.paymentType.ToString(),
                    Student = new StudentForPayment()
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        PhoneNumber = student.PhoneNumber,
                        Guruh = new GuruhReturnDto
                        {
                            Id = guruh.Id,
                            GroupName = guruh.GroupName,
                            Weekdays = guruh.Weekdays,
                            Start = guruh.Start,
                            End = guruh.End,
                            Price = guruh.Price,
                            Duration = guruh.Duration,
                            Room = await _unitOfWork.RoomInterface.GetByIdAsync(guruh.RoomId!),
                            Teacher = await _userManager.FindByIdAsync(guruh.TeacherId!),
                            Fan = await _unitOfWork.FanRepository.GetByIdAsync(guruh?.FanId!),
                        }
                    }
                };
            }
        }

        return result;
    }
}

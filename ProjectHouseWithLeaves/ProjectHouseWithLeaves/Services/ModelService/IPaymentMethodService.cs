using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;
using System.Collections;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentDtos>> GetAllPaymentMethod();
    }
}

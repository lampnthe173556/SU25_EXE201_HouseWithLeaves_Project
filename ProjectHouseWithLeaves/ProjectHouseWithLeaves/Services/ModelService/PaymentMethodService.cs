using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly IMapper _mapper;

        public PaymentMethodService(MiniPlantStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PaymentDtos>> GetAllPaymentMethod()
        {
            var paymentMethod = await _context.PaymentMethods.Where(x => x.Status == 1).ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDtos>>(paymentMethod);
        }

        
    }
}

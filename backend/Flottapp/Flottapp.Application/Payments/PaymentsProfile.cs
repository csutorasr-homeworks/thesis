using AutoMapper;
using Flottapp.Infrastucture;

namespace Flottapp.Application.Payments
{
    class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {
            CreateMap<Domain.Payment, PaymentRowVm>();
        }
    }
}

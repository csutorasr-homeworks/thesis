using MediatR;

namespace Flottapp.Application.Registration
{
    public class CreateRegistrationForCarEvent : INotification
    {
        public Domain.Registration Registration { get; set; }
    }
}

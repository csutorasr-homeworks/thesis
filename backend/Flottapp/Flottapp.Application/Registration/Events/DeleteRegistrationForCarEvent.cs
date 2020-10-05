using MediatR;

namespace Flottapp.Application.Registration
{
    public class DeleteRegistrationForCarEvent : INotification
    {
        public string FleetId { get; set; }
        public string RegistrationId { get; set; }
        public string CarId { get; set; }
    }
}

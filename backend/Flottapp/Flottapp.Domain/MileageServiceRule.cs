using System.Linq;

namespace Flottapp.Domain
{
    public class MileageServiceRule : ServiceRule
    {
        public decimal TravelledMileage { get; set; }

        public override bool NeedsService(Car car, IDateTimeProvider dateTimeProvider)
        {
            var lastServiceTime = car.ServiceOccasions.OrderBy(x => x.CreationTime).LastOrDefault()?.CreationTime ?? car.CreationTime;
            var orederedRegistrations = car.Registrations.OrderBy(x => x.CreationTime);
            var lastRegistration = orederedRegistrations.LastOrDefault();
            var lastRegistrationBeforeService = orederedRegistrations.Where(x => x.CreationTime < lastServiceTime).LastOrDefault();
            if (lastRegistration == null || lastRegistrationBeforeService == null)
            {
                return true;
            }
            return lastRegistration.Mileage - lastRegistrationBeforeService.Mileage > TravelledMileage;
        }
    }
}

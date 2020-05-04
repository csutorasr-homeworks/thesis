namespace Flottapp.Domain
{
    public abstract class ServiceRule
    {
        public abstract bool NeedsService(Car car, IDateTimeProvider dateTimeProvider);
    }
}
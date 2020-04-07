using System;

namespace Flottapp.Infrastucture
{
    public abstract class ServiceRuleVm
    {
        public string Id { get; set; }
        public abstract ServiceRuleType Type { get; protected set; }
    }
}
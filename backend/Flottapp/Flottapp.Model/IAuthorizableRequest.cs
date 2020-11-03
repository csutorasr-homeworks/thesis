using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Model
{
    public interface IAuthorizableRequest
    {
        AuthorizationData AuthorizationData { get; set; }
    }
}

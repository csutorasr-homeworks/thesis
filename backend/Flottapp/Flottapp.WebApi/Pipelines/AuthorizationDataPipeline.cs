using Flottapp.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.WebApi.Pipelines
{
    public class AuthorizationDataPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorizationDataPipeline(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IAuthorizableRequest authorizableRequest)
            {
                if (httpContextAccessor.HttpContext.User?.Identity.IsAuthenticated ?? false)
                {
                    var claims = httpContextAccessor.HttpContext.User.Claims;
                    authorizableRequest.AuthorizationData = new AuthorizationData
                    {
                        Authority = claims.FirstOrDefault(x => x.Type == "iss")?.Value,
                        Id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    };
                }
            }
            return await next();
        }
    }
}

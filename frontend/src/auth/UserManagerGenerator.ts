import { UserManager } from 'oidc-client';

export default function userManagerGenerator(config?: {
  authority: string;
  client_id: string;
}): UserManager {
  return new UserManager({
    authority: config?.authority,
    client_id: config?.client_id,
    redirect_uri: 'http://localhost:3000/account/signin-oidc',
    silent_redirect_uri: 'http://localhost:3000/account/signin-oidc-silent',
    response_type: 'code',
    response_mode: 'query',
    scope: 'openid profile flottApi',
    post_logout_redirect_uri: 'http://localhost:3000/account/signout-oidc',
    clockSkew: 0,
    automaticSilentRenew: true,
  });
}

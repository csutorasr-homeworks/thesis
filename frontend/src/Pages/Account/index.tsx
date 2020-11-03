import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import AuthGuard from '../../auth/AuthGuard';
import NotFound from '../NotFound';
import Login from './login';
import Logout from './logout';
import Profile from './profile';
import SignInOIDC from './signin-oidc';
import SignOutOIDC from './signout-oidc';

export default function Account(): JSX.Element {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/login`}>
        <Login />
      </Route>
      <Route path={`${path}/logout`}>
        <AuthGuard>
          <Logout />
        </AuthGuard>
      </Route>
      <Route path={`${path}/profile`}>
        <AuthGuard>
          <Profile />
        </AuthGuard>
      </Route>
      <Route path={`${path}/signin-oidc`}>
        <SignInOIDC silent={false} />
      </Route>
      <Route path={`${path}/signin-oidc-silent`}>
        <SignInOIDC silent />
      </Route>
      <Route path={`${path}/signout-oidc`}>
        <SignOutOIDC />
      </Route>
      <Route>
        <NotFound />
      </Route>
    </Switch>
  );
}

import React, { PropsWithChildren } from 'react';
import { Redirect } from 'react-router-dom';

import AuthIsLoggedIn from './AuthIsLoggedIn';

export default function AuthGuard({
  children,
}: PropsWithChildren<unknown>): JSX.Element {
  return (
    <AuthIsLoggedIn>
      {(isLoggedIn) =>
        isLoggedIn ? children : <Redirect to="/account/login" />
      }
    </AuthIsLoggedIn>
  );
}

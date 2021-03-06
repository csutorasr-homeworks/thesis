import React from 'react';
import { Button } from 'react-bootstrap';
import { useHistory } from 'react-router-dom';

import AuthIsLoggedIn from './AuthIsLoggedIn';

export default function AuthToggle(): JSX.Element {
  const { push } = useHistory();
  return (
    <>
      <AuthIsLoggedIn>
        {(isLoggedIn) =>
          isLoggedIn ? (
            <Button variant="primary" onClick={() => push('/account/logout')}>
              Log out
            </Button>
          ) : (
            <Button variant="primary" onClick={() => push('/account/login')}>
              Log in
            </Button>
          )
        }
      </AuthIsLoggedIn>
    </>
  );
}

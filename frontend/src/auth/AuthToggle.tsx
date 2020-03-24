import React, { useCallback, useContext } from 'react';

import { AuthContext } from './AuthModule';
import AuthIsLoggedIn from './AuthIsLoggedIn';
import { Button } from 'react-bootstrap';

export default function AuthToggle() {
  const context = useContext(AuthContext);
  const toggleSignedIn = useCallback(() => {
    context.setLoggedIn(!context.state.isLoggedIn);
  }, [context]);
  return (
    <Button variant="primary" onClick={toggleSignedIn}>
      <AuthIsLoggedIn>
        {(isLoggedIn) => (isLoggedIn ? 'Kijelentkezés' : 'Bejelentkezés')}
      </AuthIsLoggedIn>
    </Button>
  );
}

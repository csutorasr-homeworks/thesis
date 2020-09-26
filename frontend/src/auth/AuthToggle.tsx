import React, { useCallback, useContext } from 'react';
import { Button } from 'react-bootstrap';

import AuthIsLoggedIn from './AuthIsLoggedIn';
import { AuthContext } from './AuthModule';

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

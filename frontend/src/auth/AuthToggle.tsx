import React, { useCallback, useContext } from 'react';

import { AuthContext } from './AuthModule';
import { Button } from 'react-bootstrap';

export default function AuthToggle() {
  const context = useContext(AuthContext);
  const toggleSignedIn = useCallback(() => {
    context.setLoggedIn(!context.state.isLoggedIn);
  }, [context]);
  return (
    <Button variant="primary" onClick={toggleSignedIn}>
      {context.state.isLoggedIn ? 'Kijelentkezés' : 'Bejelentkezés'}
    </Button>
  );
}

import React, { useContext, useEffect } from 'react';

import { AuthContext } from '../../auth/AuthModule';

export default function Logout(): JSX.Element {
  const { userManager } = useContext(AuthContext);
  useEffect(() => {
    userManager?.signoutRedirect();
  }, [userManager]);
  return (
    <div>
      <h1 className="mb-5">Logout</h1>
    </div>
  );
}

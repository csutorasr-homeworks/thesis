import React, { ReactNode, useContext } from 'react';

import { AuthContext } from './AuthModule';

export default function AuthIsLoggedIn({
  children,
}: {
  children: (isLoggedIn: boolean) => ReactNode;
}): JSX.Element {
  const { isLoggedIn } = useContext(AuthContext);
  return <>{children(isLoggedIn)}</>;
}

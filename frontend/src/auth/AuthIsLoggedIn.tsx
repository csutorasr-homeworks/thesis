import React, { ReactNode, useContext } from 'react';

import { AuthContext } from './AuthModule';

export default function AuthIsLoggedIn({
  children,
}: {
  children: (isLoggedIn: boolean) => ReactNode;
}): JSX.Element {
  const { state } = useContext(AuthContext);
  return <>{children(state.isLoggedIn)}</>;
}

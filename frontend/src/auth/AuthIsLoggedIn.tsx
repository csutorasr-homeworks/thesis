import React, { ReactNode, useContext } from 'react';

import { AuthContext } from './AuthModule';

export default function AuthIsLoggedIn(props: {
  children: (isLoggedIn: boolean) => ReactNode;
}) {
  const context = useContext(AuthContext);
  return <>{props.children(context.state.isLoggedIn)}</>;
}

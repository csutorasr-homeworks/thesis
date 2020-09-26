import React, { createContext, useState } from 'react';

interface AuthState {
  isLoggedIn: boolean;
}

interface AuthContextData {
  setLoggedIn: (isLoggedIn: boolean) => void;
  state: AuthState;
}

const initialValue: AuthState = {
  isLoggedIn: false,
};

export const AuthContext = createContext<AuthContextData>({
  setLoggedIn: () => {},
  state: initialValue,
});

export default function AuthModule({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element {
  const [state, setState] = useState(initialValue);
  const value: AuthContextData = {
    state,
    setLoggedIn: (isLoggedIn) => {
      setState({
        ...state,
        isLoggedIn,
      });
    },
  };
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

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

export default function AuthModule(props: { children: React.ReactNode }) {
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
  return (
    <AuthContext.Provider value={value}>{props.children}</AuthContext.Provider>
  );
}

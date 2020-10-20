import Axios from 'axios';
import useAxios, { configure } from 'axios-hooks';
import { User, UserManager } from 'oidc-client';
import React, { createContext, useCallback, useEffect, useState } from 'react';

import userManagerGenerator from './UserManagerGenerator';

export const axios = Axios.create({
  baseURL: '/api',
  headers: {
    Accept: 'application/json',
  },
});

configure({ axios, cache: false });

interface AuthState {
  token?: string;
  type?: string;
}

interface AuthContextData {
  createUserManagerForAccount: (user: string) => void;
  isLoggedIn: boolean;
  userManager?: UserManager;
}

const initialValue: AuthState = {};

export const AuthContext = createContext<AuthContextData>({
  createUserManagerForAccount: () => {},
  isLoggedIn: false,
});

export default function AuthModule({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element {
  const [tokenData, setTokenData] = useState(initialValue);
  const [userManager, setUserManager] = useState<UserManager | undefined>(
    userManagerGenerator()
  );
  const [, loadConfig] = useAxios<{
    authority: string;
    client_id: string;
  }>('/account', { manual: true });
  useEffect(() => {
    axios.defaults.headers.Authorization = `${tokenData.type} ${tokenData.token}`;
  }, [tokenData]);
  useEffect(() => {
    if (!userManager) {
      return () => {};
    }
    const userManagerInstance = userManager;
    const callback = (user: User | null) => {
      setTokenData({
        token: user?.access_token,
        type: user?.token_type,
      });
    };
    userManagerInstance.events.addUserLoaded(callback);
    return () => {
      userManagerInstance.events.removeUserLoaded(callback);
    };
  }, [userManager]);
  const value: AuthContextData = {
    isLoggedIn: !!tokenData.type,
    createUserManagerForAccount: useCallback(
      async (user: string) => {
        const accountConfig = await loadConfig({
          params: { account: user },
        });
        localStorage.setItem(
          'userManagerConfig',
          JSON.stringify(accountConfig.data)
        );
        const newUserManager = userManagerGenerator(accountConfig.data);
        setUserManager(newUserManager);
        newUserManager.clearStaleState();
        newUserManager.signinRedirect();
      },
      [loadConfig]
    ),
    userManager,
  };
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

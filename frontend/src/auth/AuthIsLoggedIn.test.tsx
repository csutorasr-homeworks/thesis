import { render } from '@testing-library/react';
import React from 'react';

import AuthIsLoggedIn from './AuthIsLoggedIn';
import { AuthContext } from './AuthModule';

test('callbacks false if not logged in', () => {
  const callback = jest.fn();
  function TestStub() {
    return (
      <AuthContext.Provider
        value={{
          isLoggedIn: false,
          createUserManagerForAccount: () => {},
        }}
      >
        <AuthIsLoggedIn>{callback}</AuthIsLoggedIn>
      </AuthContext.Provider>
    );
  }
  render(<TestStub />);
  expect(callback).lastCalledWith(false);
});

test('callbacks true if logged in', () => {
  const callback = jest.fn();
  function TestStub() {
    return (
      <AuthContext.Provider
        value={{
          isLoggedIn: true,
          createUserManagerForAccount: () => {},
        }}
      >
        <AuthIsLoggedIn>{callback}</AuthIsLoggedIn>
      </AuthContext.Provider>
    );
  }
  render(<TestStub />);
  expect(callback).lastCalledWith(true);
});

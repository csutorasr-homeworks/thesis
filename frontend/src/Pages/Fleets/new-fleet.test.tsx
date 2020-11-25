import { fireEvent, render, screen } from '@testing-library/react';
import Axios from 'axios';
import { configure } from 'axios-hooks';
import { createMemoryHistory } from 'history';
import { rest } from 'msw';
import { setupServer } from 'msw/node';
import React from 'react';
import { Route, Router, Switch } from 'react-router-dom';

import NewFleet from './new-fleet';

const server = setupServer(
  rest.post('/api/fleets', (req, res, ctx) => {
    return res(ctx.json('Id'));
  })
);

const axios = Axios.create({
  baseURL: '/api',
  headers: {
    Accept: 'application/json',
  },
});

configure({ axios, cache: false });

beforeAll(() => server.listen());

afterEach(() => server.resetHandlers());
afterAll(() => server.close());

test('redirects to newly created fleet', async () => {
  const history = createMemoryHistory();
  render(
    <Router history={history}>
      <Switch>
        <Route path="/" exact>
          <NewFleet />
        </Route>
        <Route>Other route</Route>
      </Switch>
    </Router>
  );
  const input = screen.getByLabelText(/fleet name/i);
  expect(input).toBeInTheDocument();
  fireEvent.input(input, { target: { value: 'Test fleet' } });
  fireEvent.submit(screen.getByText(/submit/i));
  await screen.findByText('Other route');
  expect(history.location.pathname).toBe('/fleets/Id');
});

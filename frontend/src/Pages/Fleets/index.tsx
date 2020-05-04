import { Route, Switch, useRouteMatch } from 'react-router-dom';
import NewFleet from './new-fleet';
import NotFound from '../NotFound';
import React from 'react';

export default function Fleets() {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <NewFleet />
      </Route>
      <Route path={`${path}/:fleetId`}></Route>
      <Route>
        <NotFound />
      </Route>
    </Switch>
  );
}

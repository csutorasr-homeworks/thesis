import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import LazyWrapper from '../../Components/LazyWrapper';
import NotFound from '../NotFound';
import FleetSingle from './fleet-single';
import NewFleet from './new-fleet';

export default function Fleets(): JSX.Element {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <NewFleet />
      </Route>
      <Route path={`${path}/:fleetId/edit-users`}>
        <LazyWrapper module={() => import('./fleet-edit-user')} />
      </Route>
      <Route path={`${path}/:fleetId/edit`}>
        <LazyWrapper module={() => import('./fleet-edit')} />
      </Route>
      <Route path={`${path}/:fleetId`}>
        <Switch>
          <Route path={`${path}/:fleetId/cars`}>
            <LazyWrapper module={() => import('./Cars')} />
          </Route>
          <Route>
            <FleetSingle />
          </Route>
        </Switch>
      </Route>
      <Route>
        <NotFound />
      </Route>
    </Switch>
  );
}

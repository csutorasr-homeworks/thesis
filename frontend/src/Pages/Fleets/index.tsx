import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import NotFound from '../NotFound';
import Cars from './Cars';
import EditFleet from './edit-fleet';
import FleetSingle from './fleet-single';
import NewFleet from './new-fleet';

export default function Fleets(): JSX.Element {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <NewFleet />
      </Route>
      <Route path={`${path}/:fleetId/edit`}>
        <EditFleet />
      </Route>
      <Route path={`${path}/:fleetId`}>
        <Switch>
          <Route path={`${path}/:fleetId/cars`}>
            <Cars />
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

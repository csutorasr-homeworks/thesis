import { Route, Switch, useRouteMatch } from 'react-router-dom';
import EditFleet from './edit-fleet';
import FleetSingle from './fleet-single';
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
      <Route path={`${path}/:fleetId/edit`}>
        <EditFleet />
      </Route>
      <Route path={`${path}/:fleetId`}>
        <FleetSingle />
      </Route>
      <Route>
        <NotFound />
      </Route>
    </Switch>
  );
}

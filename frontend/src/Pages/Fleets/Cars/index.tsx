import { Route, Switch, useRouteMatch } from 'react-router-dom';
import CarNew from './car-new';
import CarSingle from './car-single';
import NotFound from '../../NotFound';
import React from 'react';

export default function Cars() {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <CarNew />
      </Route>
      <Route path={`${path}/:carId/edit`}></Route>
      <Route path={`${path}/:carId`}>
        <Switch>
          <Route>
            <CarSingle />
          </Route>
        </Switch>
      </Route>
      <Route>
        <NotFound />
      </Route>
    </Switch>
  );
}

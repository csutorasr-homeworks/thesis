import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import NotFound from '../../NotFound';
import CarEdit from './car-edit';
import CarNew from './car-new';
import CarSingle from './car-single';

export default function Cars() {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <CarNew />
      </Route>
      <Route path={`${path}/:carId/edit`}>
        <CarEdit />
      </Route>
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

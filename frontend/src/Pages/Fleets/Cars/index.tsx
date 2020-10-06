import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';

import NotFound from '../../NotFound';
import CarEdit from './car-edit';
import CarNew from './car-new';
import CarSingle from './car-single';
import CarRegistrationNew from './registrations/car-registration-new';
import CarMileageSerivceRuleNew from './service-rules/car-mileage-service-rule-new';
import CarServiceRulesList from './service-rules/car-service-rules-list';
import CarTimeSerivceRuleNew from './service-rules/car-time-service-rule-new';

export default function Cars(): JSX.Element {
  const { path } = useRouteMatch();
  return (
    <Switch>
      <Route path={`${path}/new`}>
        <CarNew />
      </Route>
      <Route path={`${path}/:carId`}>
        <Switch>
          <Route path={`${path}/:carId/edit`}>
            <CarEdit />
          </Route>
          <Route path={`${path}/:carId/add-registration`}>
            <CarRegistrationNew />
          </Route>
          {/* TODO: move to service-rules/index.tsx */}
          <Route path={`${path}/:carId/service-rules/new-time`}>
            <CarTimeSerivceRuleNew />
          </Route>
          <Route path={`${path}/:carId/service-rules/new-mileage`}>
            <CarMileageSerivceRuleNew />
          </Route>
          <Route path={`${path}/:carId/service-rules`}>
            <CarServiceRulesList />
          </Route>
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

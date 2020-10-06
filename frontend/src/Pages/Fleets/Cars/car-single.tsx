import { faEdit, faPlus } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, { useCallback, useRef } from 'react';
import { Button, ButtonGroup, Row } from 'react-bootstrap';
import { Redirect, useHistory, useParams } from 'react-router-dom';

import ErrorComponent from '../../../Components/Error';
import { CarRowVm } from './cars-list';
import MonthlyAggregateList, {
  MonthlyAggregateListHandles,
} from './monthly-aggregate/monthly-aggreage-list';
import PaymentsList, { PaymentsListHandles } from './payments/payments-list';
import CarRegistrationList from './registrations/car-registartion-list';

export default function CarSingle(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const history = useHistory();
  const [{ data: car, loading, error }, refetch] = useAxios<CarRowVm>(
    `/fleets/${fleetId}/cars/${carId}`
  );
  const [
    { loading: deleting, error: deleteError, response: deleted },
    deactivateCar,
  ] = useAxios<{
    id: string;
    name: string;
  }>(
    {
      method: 'DELETE',
      url: `/fleets/${fleetId}/cars/${carId}`,
    },
    {
      manual: true,
    }
  );
  const aggregateListRef = useRef<MonthlyAggregateListHandles>(null);
  const registrationRemoved = useCallback(() => {
    aggregateListRef.current?.refetch();
  }, [aggregateListRef]);
  const paymentListRef = useRef<PaymentsListHandles>(null);
  const monthlyAggregateAccepted = useCallback(() => {
    paymentListRef.current?.refetch();
  }, [paymentListRef]);
  if (deleted) {
    return <Redirect to={`/fleets/${fleetId}`} />;
  }
  return (
    <ErrorComponent
      loading={loading || deleting}
      error={error || deleteError}
      refetch={refetch}
    >
      {() => (
        <>
          <Row className="mb-4">
            <h1 className="col">
              {car?.licensePlateNumber}
              <span className="subheader">car</span>
            </h1>
            <ButtonGroup
              style={{ alignSelf: 'center' }}
              className="col flex-grow-0"
            >
              <Button
                onClick={() =>
                  history.push(`/fleets/${fleetId}/cars/${carId}/edit`)
                }
              >
                <FontAwesomeIcon icon={faEdit} />
              </Button>
              <Button onClick={() => deactivateCar()} variant="danger">
                Deactivate
              </Button>
            </ButtonGroup>
          </Row>
          <Row className="mb-2">
            <h2 className="col">Registrations</h2>
            <ButtonGroup
              style={{ alignSelf: 'center' }}
              className="col flex-grow-0"
            >
              <Button
                onClick={() =>
                  history.push(
                    `/fleets/${fleetId}/cars/${carId}/add-registration`
                  )
                }
              >
                <FontAwesomeIcon icon={faPlus} />
              </Button>
            </ButtonGroup>
          </Row>
          <div className="mb-3">
            <CarRegistrationList registrationRemoved={registrationRemoved} />
          </div>
          <Row className="mb-2">
            <h2 className="col">Monthly aggregates</h2>
          </Row>
          <div className="mb-3">
            <MonthlyAggregateList
              ref={aggregateListRef}
              monthlyAggregateAccepted={monthlyAggregateAccepted}
            />
          </div>
          <Row className="mb-2">
            <h2 className="col">Payments</h2>
          </Row>
          <div className="mb-3">
            <PaymentsList ref={paymentListRef} />
          </div>
        </>
      )}
    </ErrorComponent>
  );
}

import { Button, ButtonGroup, Row } from 'react-bootstrap';
import { Redirect, useHistory, useParams } from 'react-router-dom';
import { CarRowVm } from './cars-list';
import ErrorComponent from '../../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function CarSingle() {
  const { fleetId, carId } = useParams();
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
          <Row>
            <h1 className="col">{car?.licensePlateNumber}</h1>
            <ButtonGroup style={{ alignSelf: 'center' }}>
              <Button
                onClick={() =>
                  history.push(`/fleets/${fleetId}/cars/${carId}/edit`)
                }
              >
                Edit
              </Button>
              <Button onClick={() => deactivateCar()} variant="danger">
                Deactivate
              </Button>
            </ButtonGroup>
          </Row>
        </>
      )}
    </ErrorComponent>
  );
}

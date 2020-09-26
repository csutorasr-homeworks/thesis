import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React from 'react';
import { Button, ButtonGroup, Row } from 'react-bootstrap';
import { Redirect, useHistory, useParams } from 'react-router-dom';

import ErrorComponent from '../../../Components/Error';
import { CarRowVm } from './cars-list';

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
          <Row className="mb-5">
            <h1 className="col">
              {car?.licensePlateNumber}
              <span className="subheader">car</span>
            </h1>
            <ButtonGroup style={{ alignSelf: 'center' }}>
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
        </>
      )}
    </ErrorComponent>
  );
}

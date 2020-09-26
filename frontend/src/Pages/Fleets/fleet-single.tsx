import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React from 'react';
import { Button, ButtonGroup, Row } from 'react-bootstrap';
import { Redirect, useHistory, useParams } from 'react-router-dom';

import ErrorComponent from '../../Components/Error';
import CarsList from './Cars/cars-list';

export default function FleetSingle(): JSX.Element {
  const { fleetId } = useParams<{ fleedId: string }>();
  const history = useHistory();
  const [{ data: fleet, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
  }>(`/fleets/${fleetId}`);
  const [
    { loading: deleting, error: deleteError, response: deleted },
    deleteFleet,
  ] = useAxios<{
    id: string;
    name: string;
  }>(
    {
      method: 'DELETE',
      url: `/fleets/${fleetId}`,
    },
    {
      manual: true,
    }
  );
  if (deleted) {
    return <Redirect to="/" />;
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
              {fleet?.name}
              <span className="subheader">fleet</span>
            </h1>
            <ButtonGroup style={{ alignSelf: 'center' }}>
              <Button onClick={() => history.push(`/fleets/${fleetId}/edit`)}>
                <FontAwesomeIcon icon={faEdit} />
              </Button>
              <Button onClick={() => deleteFleet()} variant="danger">
                <FontAwesomeIcon icon={faTrash} />
              </Button>
            </ButtonGroup>
          </Row>
          <CarsList />
        </>
      )}
    </ErrorComponent>
  );
}

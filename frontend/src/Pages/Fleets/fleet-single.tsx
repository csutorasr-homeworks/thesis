import { Button, ButtonGroup, Row } from 'react-bootstrap';
import { Redirect, useHistory, useParams } from 'react-router-dom';
import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function FleetSingle() {
  const { fleetId } = useParams();
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
        <Row>
          <h1 className="col">{fleet?.name}</h1>
          <ButtonGroup style={{ alignSelf: 'center' }}>
            <Button onClick={() => history.push(`/fleets/${fleetId}/edit`)}>
              Edit
            </Button>
            <Button onClick={() => deleteFleet()} variant="danger">
              Delete
            </Button>
          </ButtonGroup>
        </Row>
      )}
    </ErrorComponent>
  );
}

import { Link, Redirect, useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function FleetSingle() {
  const { fleetId } = useParams();
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
          <h1>{fleet?.name}</h1>
          <Link to={`/fleets/${fleetId}/edit`}>Edit</Link>
          <Button onClick={() => deleteFleet()}>Delete</Button>
        </>
      )}
    </ErrorComponent>
  );
}

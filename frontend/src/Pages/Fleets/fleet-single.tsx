import { Link, useParams } from 'react-router-dom';
import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function FleetSingle() {
  const { fleetId } = useParams();
  const [{ data: fleet, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
  }>(`/fleets/${fleetId}`);
  return (
    <>
      <ErrorComponent loading={loading} error={error} refetch={refetch} />
      <h1>{fleet?.name}</h1>
      <Link to={`/fleets/${fleetId}/edit`}>Edit</Link>
    </>
  );
}

import { Link, Redirect } from 'react-router-dom';
import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function FleetList() {
  const [{ data: fleets, loading, error }, refetch] = useAxios<
    { id: string; name: string }[]
  >('/fleets');
  if (fleets && fleets.length === 1) {
    return <Redirect to={`/fleets/${fleets[0].id}`} />;
  }
  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <ul>
          {fleets.map((x) => (
            <li key={x.id}>
              <Link to={`/fleets/${x.id}`}>{x.name}</Link>
            </li>
          ))}
        </ul>
      )}
    </ErrorComponent>
  );
}

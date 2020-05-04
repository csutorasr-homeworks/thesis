import ErrorComponent from '../../Components/Error';
import React from 'react';
import { Redirect } from 'react-router-dom';
import useAxios from 'axios-hooks';

export default function Home() {
  const [{ data: fleets, loading, error }, refetch] = useAxios<
    { id: string; name: string }[]
  >('/fleets');
  if (fleets && fleets.length === 1) {
    return <Redirect to={`/fleets/${fleets[0].id}`} />;
  }
  return (
    <div>
      Home
      <ErrorComponent loading={loading} error={error} refetch={refetch} />
      <ul>
        {!loading && !error && fleets.map((x) => <li key={x.id}>{x.name}</li>)}
      </ul>
    </div>
  );
}

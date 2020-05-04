import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function Home() {
  const [{ data: fleets, loading, error }, refetch] = useAxios<
    { id: string; name: string }[]
  >('/fleets');
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

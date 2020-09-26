import React from 'react';

import FleetList from '../Fleets/fleet-list';

export default function Home(): JSX.Element {
  return (
    <div>
      <h1 className="mb-5">Fleets</h1>
      <FleetList />
    </div>
  );
}

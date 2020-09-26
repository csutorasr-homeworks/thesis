import FleetList from '../Fleets/fleet-list';
import React from 'react';

export default function Home() {
  return (
    <div>
      <h1 className="mb-5">Fleets</h1>
      <FleetList />
    </div>
  );
}

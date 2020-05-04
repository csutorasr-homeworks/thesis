import { Link, useParams } from 'react-router-dom';
import React from 'react';

export default function FleetSingle() {
  const { fleetId } = useParams();
  return <Link to={`/fleets/${fleetId}/edit`}>Edit</Link>;
}

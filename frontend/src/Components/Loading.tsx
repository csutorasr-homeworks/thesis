import React from 'react';
import { Spinner } from 'react-bootstrap';

export default function Loading(): JSX.Element {
  return (
    <div style={{ display: 'flex', justifyContent: 'center' }}>
      <Spinner animation="grow" />
    </div>
  );
}

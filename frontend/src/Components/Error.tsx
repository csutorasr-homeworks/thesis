import { Alert, Button } from 'react-bootstrap';
import React from 'react';

export default function ErrorComponent({
  loading,
  error,
  refetch,
}: {
  loading: boolean;
  error: any;
  refetch?: () => void;
}) {
  return (
    !loading &&
    error && (
      <Alert variant="danger">
        <Alert.Heading>An error occoured</Alert.Heading>
        {error.message}
        <hr />
        {refetch && <Button onClick={() => refetch()}>reload</Button>}
      </Alert>
    )
  );
}

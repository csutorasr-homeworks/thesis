import { Alert, Button } from 'react-bootstrap';
import React, { ReactNode } from 'react';

export default function ErrorComponent({
  loading,
  error,
  refetch,
  children,
}: {
  loading: boolean;
  error: any;
  refetch?: () => void;
  children: ReactNode;
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

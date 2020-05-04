import { Alert, Button } from 'react-bootstrap';
import React, { ReactElement } from 'react';

export default function ErrorComponent({
  loading,
  error,
  refetch,
  children,
}: {
  loading: boolean;
  error: any;
  refetch?: () => void;
  children?: () => ReactElement;
}) {
  if (loading) {
    // TODO: load page
    return null;
  }
  if (error) {
    return (
      <Alert variant="danger">
        <Alert.Heading>An error occoured</Alert.Heading>
        {error.message}
        <hr />
        {refetch && <Button onClick={() => refetch()}>reload</Button>}
      </Alert>
    );
  }
  if (children) {
    return children();
  }
  return null;
}

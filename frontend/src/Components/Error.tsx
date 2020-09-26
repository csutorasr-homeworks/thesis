import React, { ReactElement } from 'react';
import { Alert, Button } from 'react-bootstrap';

import Loading from './Loading';

export default function ErrorComponent({
  loading,
  error,
  refetch,
  children,
}: {
  loading: boolean;
  error: Error | undefined;
  refetch?: () => void;
  children?: () => ReactElement;
}): JSX.Element | null {
  if (loading) {
    return <Loading />;
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

ErrorComponent.defaultProps = {
  refetch: undefined,
  children: undefined,
};

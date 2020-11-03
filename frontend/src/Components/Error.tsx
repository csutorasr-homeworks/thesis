import { AxiosError } from 'axios';
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
  error: Error | AxiosError<unknown> | undefined;
  refetch?: () => void;
  children?: () => ReactElement;
}): JSX.Element | null {
  if (loading) {
    return <Loading />;
  }
  if (error) {
    let text = error.message;
    const data = (error as AxiosError<unknown>).response?.data;
    if (typeof data === 'string') {
      text = data;
    }
    return (
      <Alert variant="danger">
        <Alert.Heading>An error occoured</Alert.Heading>
        {text}
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

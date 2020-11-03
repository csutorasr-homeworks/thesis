import useAxios from 'axios-hooks';
import React, { useMemo } from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect } from 'react-router-dom';

import ErrorComponent from '../../Components/Error';

export default function Profile(): JSX.Element {
  const [{ data: account, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
  }>(`/account/profile`);
  const not404Error = useMemo(
    () => (error?.response?.status === 404 ? undefined : error),
    [error]
  );

  const { handleSubmit, control } = useForm<{
    name: string;
  }>();
  const [{ loading: saveLoading, response: edited }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/account/profile`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: { name: string }) => {
    if (!saveLoading) {
      send({
        data,
      });
    }
  };
  if (edited) {
    return <Redirect to="/" />;
  }

  return (
    <ErrorComponent loading={loading} error={not404Error} refetch={refetch}>
      {() => (
        <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
          <Form.Group controlId="formName">
            <Form.Label>Profile name</Form.Label>
            <Controller
              as={
                <Form.Control name="name" placeholder="Enter name" required />
              }
              control={control}
              rules={{ required: true }}
              name="name"
              defaultValue={account?.name}
            />
            <Form.Control.Feedback type="invalid">
              This field is required
            </Form.Control.Feedback>
          </Form.Group>
          <Button variant="primary" type="submit" disabled={saveLoading}>
            Submit
          </Button>
        </Form>
      )}
    </ErrorComponent>
  );
}

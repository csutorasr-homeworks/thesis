import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

import ErrorComponent from '../../Components/Error';

export default function FleetEdit(): JSX.Element {
  const { fleetId } = useParams<{ fleetId: string }>();
  const [{ data: fleet, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
  }>(`/fleets/${fleetId}`);
  const { handleSubmit, control } = useForm<{
    name: string;
  }>();
  const [{ loading: saveLoading, response: edited }, send] = useAxios<string>(
    {
      method: 'PUT',
      url: `/fleets/${fleetId}`,
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
    return <Redirect to={`/fleets/${fleetId}`} />;
  }

  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
          <Form.Group controlId="formName">
            <Form.Label>Fleet name</Form.Label>
            <Controller
              as={
                <Form.Control name="name" placeholder="Enter name" required />
              }
              control={control}
              rules={{ required: true }}
              name="name"
              defaultValue={fleet.name}
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

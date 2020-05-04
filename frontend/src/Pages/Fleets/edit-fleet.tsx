import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import React, { useState } from 'react';
import { Redirect, useParams } from 'react-router-dom';
import useAxios from 'axios-hooks';
import ErrorComponent from '../../Components/Error';

export default function EditFleet() {
  const { fleetId } = useParams();
  const [{ data: fleet, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
  }>(`/fleets/${fleetId}`);
  const { handleSubmit, control } = useForm<{
    name: string;
  }>();
  const [editSuccessful, setEditSuccessful] = useState(false);
  const [{ loading: saveLoading }, send] = useAxios<string>(
    {
      method: 'PUT',
      url: `/fleets/${fleetId}`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = async (data: { name: string }) => {
    if (!saveLoading) {
      await send({
        data,
      });
      setEditSuccessful(true);
    }
  };
  if (loading || error) {
    return <ErrorComponent loading={loading} error={error} refetch={refetch} />;
  }
  if (editSuccessful) {
    return <Redirect to={`/fleets/${fleetId}`} />;
  }

  return (
    <Form noValidate validated={true} onSubmit={handleSubmit(onSubmit)}>
      <Form.Group controlId="formBasicEmail">
        <Form.Label>Fleet name</Form.Label>
        <Controller
          as={<Form.Control name="name" placeholder="Enter name" required />}
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
  );
}

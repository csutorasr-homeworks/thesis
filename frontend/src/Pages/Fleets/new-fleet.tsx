import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect } from 'react-router-dom';

export default function NewFleet(): JSX.Element {
  const { handleSubmit, control } = useForm<{
    name: string;
  }>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: '/fleets',
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: { name: string }) => {
    if (!loading) {
      send({
        data,
      });
    }
  };
  if (createdId) {
    return <Redirect to={`/fleets/${createdId}`} />;
  }

  return (
    <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
      <Form.Group controlId="formName">
        <Form.Label>Fleet name</Form.Label>
        <Controller
          as={<Form.Control name="name" placeholder="Enter name" required />}
          control={control}
          rules={{ required: true }}
          name="name"
          defaultValue=""
        />
        <Form.Control.Feedback type="invalid">
          This field is required
        </Form.Control.Feedback>
      </Form.Group>
      <Button variant="primary" type="submit" disabled={loading}>
        Submit
      </Button>
    </Form>
  );
}

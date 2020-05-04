import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';
import React from 'react';
import useAxios from 'axios-hooks';

interface FormData {
  licensePlateNumber: string;
  limitPerMonth: number;
}

export default function CarNew() {
  const { fleetId } = useParams();
  const { handleSubmit, control } = useForm<FormData>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/cars`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!loading) {
      send({
        data: {
          limitPerMonth: {
            currency: 'HUF',
            value: +data.limitPerMonth,
          },
          licensePlateNumber: data.licensePlateNumber,
        },
      });
    }
  };
  if (createdId) {
    return <Redirect to={`/fleets/${fleetId}/cars/${createdId}`} />;
  }

  return (
    <Form noValidate validated={true} onSubmit={handleSubmit(onSubmit)}>
      <Form.Group controlId="formLimitPerMonth">
        <Form.Label>Limit per month</Form.Label>
        <Controller
          as={
            <Form.Control
              name="limitPerMonth"
              type="number"
              placeholder="Enter limit"
              required
            />
          }
          control={control}
          rules={{ required: true }}
          name="limitPerMonth"
          defaultValue=""
        />
        <Form.Control.Feedback type="invalid">
          This field is required
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group controlId="formLicensePlateNumber">
        <Form.Label>License Plate Number</Form.Label>
        <Controller
          as={
            <Form.Control
              name="licensePlateNumber"
              placeholder="Enter license plate number"
              required
            />
          }
          control={control}
          rules={{ required: true }}
          name="licensePlateNumber"
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

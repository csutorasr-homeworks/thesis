import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

interface FormData {
  dateTime: Date;
  mileage: number;
  type: 0;
}

export default function ServiceOccasionNew(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const { handleSubmit, control } = useForm<FormData>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/cars/${carId}/service-occasions`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!loading) {
      send({
        data: {
          dateTime: new Date(data.dateTime),
          mileage: +data.mileage,
          type: 0,
        },
      });
    }
  };
  if (createdId) {
    return <Redirect to={`/fleets/${fleetId}/cars/${carId}`} />;
  }

  return (
    <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
      <div className="row">
        <Form.Group controlId="mileage" className="col-lg-6">
          <Form.Label>Mileage</Form.Label>
          <Controller
            as={
              <Form.Control
                name="mileage"
                type="number"
                placeholder="Enter mileage"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="mileage"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="dateTime" className="col-lg-6">
          <Form.Label>Date/time</Form.Label>
          <Controller
            as={
              <Form.Control
                name="dateTime"
                type="date"
                placeholder="Enter date"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="dateTime"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
      </div>
      <Button variant="primary" type="submit" disabled={loading}>
        Submit
      </Button>
    </Form>
  );
}

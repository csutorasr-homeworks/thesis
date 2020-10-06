import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

interface FormData {
  intervalInMonth: number;
}

export default function CarTimeSerivceRuleNew(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const { handleSubmit, control } = useForm<FormData>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/cars/${carId}/service-rules/time`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!loading) {
      send({
        data: {
          intervalInMonth: +data.intervalInMonth,
        },
      });
    }
  };
  if (createdId) {
    return <Redirect to={`/fleets/${fleetId}/cars/${carId}/service-rules`} />;
  }

  return (
    <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
      <div className="row">
        <Form.Group controlId="intervalInMonth" className="col-lg-6">
          <Form.Label>Interval in months</Form.Label>
          <Controller
            as={
              <Form.Control
                name="intervalInMonth"
                type="number"
                placeholder="Enter interval"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="intervalInMonth"
            defaultValue="12"
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

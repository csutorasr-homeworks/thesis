import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

interface FormData {
  travelledMileage: number;
}

export default function CarMileageSerivceRuleNew(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const { handleSubmit, control } = useForm<FormData>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/cars/${carId}/service-rules/mileage`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!loading) {
      send({
        data: {
          travelledMileage: +data.travelledMileage,
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
        <Form.Group controlId="travelledMileage" className="col-lg-6">
          <Form.Label>Travelled mileage</Form.Label>
          <Controller
            as={
              <Form.Control
                name="travelledMileage"
                type="number"
                placeholder="Enter mileage"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="travelledMileage"
            defaultValue="30000"
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

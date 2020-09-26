import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

import ErrorComponent from '../../../Components/Error';

interface FormData {
  licensePlateNumber: string;
  limitPerMonth: number;
}

export default function CarEdit() {
  const { fleetId, carId } = useParams();
  const [{ data: car, loading, error }, refetch] = useAxios<{
    licensePlateNumber: string;
    limitPerMonth: {
      currency: string;
      value: number;
    };
  }>(`/fleets/${fleetId}/cars/${carId}`);
  const { handleSubmit, control } = useForm<FormData>();
  const [{ loading: saveLoading, response: edited }, send] = useAxios<string>(
    {
      method: 'PUT',
      url: `/fleets/${fleetId}/cars/${carId}`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!saveLoading) {
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
  if (edited) {
    return <Redirect to={`/fleets/${fleetId}/cars/${carId}`} />;
  }

  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <Form noValidate validated={true} onSubmit={handleSubmit(onSubmit)}>
          <div className="row">
            <Form.Group controlId="formLimitPerMonth" className="col-lg-6">
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
                defaultValue={car.limitPerMonth.value}
              />
              <Form.Control.Feedback type="invalid">
                This field is required
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group controlId="formLicensePlateNumber" className="col-lg-6">
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
                defaultValue={car.licensePlateNumber}
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
      )}
    </ErrorComponent>
  );
}

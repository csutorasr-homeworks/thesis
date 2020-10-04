import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { Redirect, useParams } from 'react-router-dom';

interface BackendData {
  time: Date;
  mileage: number;
  location: {
    longitude: number;
    langitude: number;
  };
  refuelQuantity: number;
  price: {
    currency: number;
    value: number;
  };
}

interface FormData {
  time: Date;
  mileage: number;
  locationlongitude: number;
  locationlangitude: number;
  refuelQuantity: number;
  pricecurrency: number;
  pricevalue: number;
}

export default function CarRegistrationNew(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const { handleSubmit, control } = useForm<FormData>();
  const [{ data: createdId, loading }, send] = useAxios<string>(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/cars/${carId}/registrations`,
    },
    {
      manual: true,
    }
  );
  const onSubmit = (data: FormData) => {
    if (!loading) {
      send({
        data: {
          location: {
            langitude: +data.locationlangitude,
            longitude: +data.locationlongitude,
          },
          mileage: +data.mileage,
          price: {
            currency: data.pricecurrency,
            value: +data.pricevalue,
          },
          refuelQuantity: +data.refuelQuantity,
          time: new Date(data.time),
        } as BackendData,
      });
    }
  };
  if (createdId) {
    return <Redirect to={`/fleets/${fleetId}/cars/${carId}`} />;
  }

  return (
    <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
      <div className="row">
        <Form.Group controlId="locationlangitude" className="col-lg-6">
          <Form.Label>Langitude</Form.Label>
          <Controller
            as={
              <Form.Control
                name="locationlangitude"
                type="number"
                placeholder="Enter langitude"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="locationlangitude"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="locationlongitude" className="col-lg-6">
          <Form.Label>Longitude</Form.Label>
          <Controller
            as={
              <Form.Control
                name="locationlongitude"
                type="number"
                placeholder="Enter longitude"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="locationlongitude"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
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
        <Form.Group controlId="pricecurrency" className="col-lg-6">
          <Form.Label>Currency</Form.Label>
          <Controller
            as={
              <Form.Control
                name="pricecurrency"
                type="text"
                placeholder="Enter currency"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="pricecurrency"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="pricevalue" className="col-lg-6">
          <Form.Label>Price</Form.Label>
          <Controller
            as={
              <Form.Control
                name="pricevalue"
                type="number"
                placeholder="Enter price"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="pricevalue"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="refuelQuantity" className="col-lg-6">
          <Form.Label>Refuel quantity</Form.Label>
          <Controller
            as={
              <Form.Control
                name="refuelQuantity"
                type="number"
                placeholder="Enter refuel quantity"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="refuelQuantity"
            defaultValue=""
          />
          <Form.Control.Feedback type="invalid">
            This field is required
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="time" className="col-lg-6">
          <Form.Label>Date/time</Form.Label>
          <Controller
            as={
              <Form.Control
                name="time"
                type="date"
                placeholder="Enter date"
                required
              />
            }
            control={control}
            rules={{ required: true }}
            name="time"
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

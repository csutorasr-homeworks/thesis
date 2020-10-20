import React, { useCallback, useContext } from 'react';
import { Button, Card, Form } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';

import { AuthContext } from '../../auth/AuthModule';

export default function Login(): JSX.Element {
  const { createUserManagerForAccount } = useContext(AuthContext);
  const { handleSubmit, control } = useForm<{
    email: string;
  }>();
  const onSubmit = useCallback(
    (data: { email: string }) => {
      createUserManagerForAccount(data.email);
    },
    [createUserManagerForAccount]
  );
  return (
    <Card>
      <Card.Body>
        <h4 className="card-title mb-4 mt-1">Log in</h4>
        <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
          <Form.Group>
            <Form.Label>Your email</Form.Label>
            <Controller
              as={
                <Form.Control name="email" placeholder="Your email" required />
              }
              control={control}
              rules={{ required: true }}
              name="email"
              defaultValue=""
            />
            <Form.Control.Feedback type="invalid">
              This field is required
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Button variant="primary" type="submit">
              Login
            </Button>
          </Form.Group>
        </Form>
      </Card.Body>
    </Card>
  );
}

import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, { useCallback } from 'react';
import { Button, Form, ListGroup } from 'react-bootstrap';
import { Controller, useForm } from 'react-hook-form';
import { useParams } from 'react-router-dom';

import ErrorComponent from '../../Components/Error';

export default function EditFleet(): JSX.Element {
  const { fleetId } = useParams<{ fleetId: string }>();
  const [{ data: fleet, loading, error }, refetch] = useAxios<{
    id: string;
    name: string;
    users: string[];
  }>(`/fleets/${fleetId}`);
  const { handleSubmit, control } = useForm<{
    userId: string;
  }>();
  const [{ loading: saveLoading, error: saveError }, addUser] = useAxios<
    string
  >(
    {
      method: 'POST',
      url: `/fleets/${fleetId}/users`,
    },
    {
      manual: true,
    }
  );
  const [{ loading: removeLoading, error: removeError }, removeUser] = useAxios<
    string
  >(
    {
      method: 'DELETE',
      url: `/fleets/${fleetId}/users/userid`,
    },
    {
      manual: true,
    }
  );
  const onRemove = useCallback(
    async (userId: string) => {
      await removeUser({ url: `/fleets/${fleetId}/users/${userId}` });
      refetch();
    },
    [fleetId, removeUser, refetch]
  );
  const onSubmit = useCallback(
    async (data: { userId: string }) => {
      if (!saveLoading) {
        await addUser({
          data,
        });
        control.setValue('userId', '');
        refetch();
      }
    },
    [control, saveLoading, addUser, refetch]
  );

  return (
    <ErrorComponent loading={loading && !fleet} error={error} refetch={refetch}>
      {() => (
        <>
          <h1>Edit users for {fleet.name}</h1>
          <h2>Current users</h2>
          <ErrorComponent loading={removeLoading} error={removeError}>
            {() => (
              <>
                {!!fleet.users.length && (
                  <ListGroup>
                    {fleet.users.map((user) => (
                      <ListGroup.Item
                        key={user}
                        style={{ display: 'flex', alignItems: 'center' }}
                      >
                        <span style={{ flex: 1 }}>{user}</span>
                        <Button onClick={() => onRemove(user)} variant="danger">
                          <FontAwesomeIcon icon={faTrash} />
                        </Button>
                      </ListGroup.Item>
                    ))}
                  </ListGroup>
                )}
                {!fleet.users.length && (
                  <div>No users currently in this fleet.</div>
                )}
              </>
            )}
          </ErrorComponent>

          <h2>Add user</h2>
          <ErrorComponent loading={saveLoading} error={saveError} />
          <Form noValidate validated onSubmit={handleSubmit(onSubmit)}>
            <Form.Group controlId="formName">
              <Form.Label>Username</Form.Label>
              <Controller
                as={
                  <Form.Control
                    name="name"
                    placeholder="Enter username"
                    required
                  />
                }
                control={control}
                rules={{ required: true }}
                name="userId"
                defaultValue=""
              />
              <Form.Control.Feedback type="invalid">
                This field is required
              </Form.Control.Feedback>
            </Form.Group>
            <Button variant="primary" type="submit" disabled={saveLoading}>
              Submit
            </Button>
          </Form>
        </>
      )}
    </ErrorComponent>
  );
}

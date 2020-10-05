import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, { useCallback } from 'react';
import { Button, ListGroup } from 'react-bootstrap';
import { useParams } from 'react-router-dom';

import ErrorComponent from '../../../../Components/Error';

export default function CarRegistrationList({
  registrationRemoved,
}: {
  registrationRemoved: () => void;
}): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const [{ data: registrations, loading, error }, refetch] = useAxios<
    {
      id: string;
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
    }[]
  >(`/fleets/${fleetId}/cars/${carId}/registrations`);
  const [
    { loading: removeLoading, error: removeError },
    removeRegistration,
  ] = useAxios<string>(
    {
      method: 'DELETE',
      url: `/fleets/${fleetId}/users/userid`,
    },
    {
      manual: true,
    }
  );
  const onRemove = useCallback(
    async (registrationId: string) => {
      await removeRegistration({
        url: `/fleets/${fleetId}/cars/${carId}/registrations/${registrationId}`,
      });
      registrationRemoved();
      refetch();
    },
    [fleetId, carId, removeRegistration, registrationRemoved, refetch]
  );

  return (
    <ErrorComponent
      loading={loading || removeLoading}
      error={error || removeError}
      refetch={refetch}
    >
      {() => (
        <>
          {!!registrations.length && (
            <ListGroup>
              {registrations.map((registration) => (
                <ListGroup.Item
                  key={registration.id}
                  style={{ display: 'flex', alignItems: 'center' }}
                >
                  <span style={{ width: '100px' }}>
                    {new Date(registration.time).toLocaleDateString()}
                  </span>
                  <span style={{ flex: 1 }}>{registration.mileage} km</span>
                  <Button
                    onClick={() => onRemove(registration.id)}
                    variant="danger"
                  >
                    <FontAwesomeIcon icon={faTrash} />
                  </Button>
                </ListGroup.Item>
              ))}
            </ListGroup>
          )}
          {!registrations.length && (
            <div>No registration currently for this car.</div>
          )}
        </>
      )}
    </ErrorComponent>
  );
}

import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, { useCallback } from 'react';
import { Button, ListGroup } from 'react-bootstrap';
import { useParams } from 'react-router-dom';

import ErrorComponent from '../../../../Components/Error';

export default function ServiceOccasionsList(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const [{ data: serviceOccasions, loading, error }, refetch] = useAxios<
    {
      id: string;
      dateTime: Date;
      mileage: number;
    }[]
  >(`/fleets/${fleetId}/cars/${carId}/service-occasions`);
  const [
    { loading: removeLoading, error: removeError },
    removeRegistration,
  ] = useAxios<string>(
    {
      method: 'DELETE',
    },
    {
      manual: true,
    }
  );
  const onRemove = useCallback(
    async (serviceOccasionId: string) => {
      await removeRegistration({
        url: `/fleets/${fleetId}/cars/${carId}/service-occasions/${serviceOccasionId}`,
      });
      refetch();
    },
    [fleetId, carId, removeRegistration, refetch]
  );

  return (
    <ErrorComponent
      loading={loading || removeLoading}
      error={error || removeError}
      refetch={refetch}
    >
      {() => (
        <>
          {!!serviceOccasions.length && (
            <ListGroup>
              {serviceOccasions.map((serviceOccasion) => (
                <ListGroup.Item
                  key={serviceOccasion.id}
                  style={{ display: 'flex', alignItems: 'center' }}
                >
                  <span style={{ width: '100px' }}>
                    {new Date(serviceOccasion.dateTime).toLocaleDateString()}
                  </span>
                  <span style={{ flex: 1 }}>{serviceOccasion.mileage} km</span>
                  <Button
                    onClick={() => onRemove(serviceOccasion.id)}
                    variant="danger"
                  >
                    <FontAwesomeIcon icon={faTrash} />
                  </Button>
                </ListGroup.Item>
              ))}
            </ListGroup>
          )}
          {!serviceOccasions.length && (
            <div>No service occasion currently for this car.</div>
          )}
        </>
      )}
    </ErrorComponent>
  );
}

import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, { useCallback } from 'react';
import { Button, ButtonGroup, ListGroup, Row } from 'react-bootstrap';
import { useHistory, useParams } from 'react-router-dom';

import ErrorComponent from '../../../../Components/Error';

enum ServiceRuleType {
  Time = 0,
  Mileage = 1,
}

interface TimeServiceRule {
  id: string;
  type: ServiceRuleType.Time;
  intervalInMonth: number;
}

interface MileageServiceRule {
  id: string;
  type: ServiceRuleType.Mileage;
  travelledMileage: number;
}

type ServiceRule = TimeServiceRule | MileageServiceRule;

export default function CarServiceRulesList(): JSX.Element {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const history = useHistory();
  const [{ data: serviceRules, loading, error }, refetch] = useAxios<
    ServiceRule[]
  >(`/fleets/${fleetId}/cars/${carId}/service-rules`);
  const [{ loading: removeLoading, error: removeError }, remove] = useAxios<
    string
  >(
    {
      method: 'DELETE',
    },
    {
      manual: true,
    }
  );
  const onRemove = useCallback(
    async (serviceRuleId: string) => {
      await remove({
        url: `/fleets/${fleetId}/cars/${carId}/service-rules/${serviceRuleId}`,
      });
      refetch();
    },
    [fleetId, carId, remove, refetch]
  );

  function renderForType(serviceRule: ServiceRule) {
    switch (serviceRule.type) {
      case ServiceRuleType.Time:
        return `${serviceRule.intervalInMonth} month(s)`;
      case ServiceRuleType.Mileage:
        return `${serviceRule.travelledMileage} km`;
      default:
        return `unknown type`;
    }
  }

  return (
    <>
      <Row className="mb-4">
        <h1 className="col">Service rules</h1>
        <ButtonGroup
          style={{ alignSelf: 'center' }}
          className="col flex-grow-0"
        >
          <Button
            onClick={() =>
              history.push(
                `/fleets/${fleetId}/cars/${carId}/service-rules/new-time`
              )
            }
          >
            <FontAwesomeIcon icon={faPlus} /> Time
          </Button>
          <Button
            onClick={() =>
              history.push(
                `/fleets/${fleetId}/cars/${carId}/service-rules/new-mileage`
              )
            }
          >
            <FontAwesomeIcon icon={faPlus} /> Mileage
          </Button>
        </ButtonGroup>
      </Row>
      <ErrorComponent
        loading={loading || removeLoading}
        error={error || removeError}
        refetch={refetch}
      >
        {() => (
          <>
            {!!serviceRules.length && (
              <ListGroup>
                {serviceRules.map((serviceRule) => (
                  <ListGroup.Item
                    key={serviceRule.id}
                    style={{ display: 'flex', alignItems: 'center' }}
                  >
                    <span style={{ flex: 1 }}>
                      {renderForType(serviceRule)}
                    </span>
                    <Button
                      onClick={() => onRemove(serviceRule.id)}
                      variant="danger"
                    >
                      <FontAwesomeIcon icon={faTrash} />
                    </Button>
                  </ListGroup.Item>
                ))}
              </ListGroup>
            )}
            {!serviceRules.length && (
              <div>No service rule currently for this car.</div>
            )}
          </>
        )}
      </ErrorComponent>
    </>
  );
}

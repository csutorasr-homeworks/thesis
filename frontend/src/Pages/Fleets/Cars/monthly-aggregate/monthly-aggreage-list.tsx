import { faCheck, faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React, {
  forwardRef,
  ForwardRefRenderFunction,
  useCallback,
  useImperativeHandle,
} from 'react';
import { Button, ListGroup } from 'react-bootstrap';
import { useParams } from 'react-router-dom';

import ErrorComponent from '../../../../Components/Error';

export interface MonthlyAggregateListHandles {
  refetch: () => void;
}

interface Props {
  monthlyAggregateAccepted: () => void;
}

const MonthlyAggregateList: ForwardRefRenderFunction<
  MonthlyAggregateListHandles,
  Props
> = ({ monthlyAggregateAccepted }: Props, ref) => {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const [{ data: monthlyAggregates, loading, error }, refetch] = useAxios<
    {
      id: string;
      year: number;
      month: number;
      sumOfPrice: {
        currency: string;
        value: number;
      };
      limit: {
        currency: string;
        value: number;
      };
      travelledDistance: number;
      accepted: boolean;
    }[]
  >(`/fleets/${fleetId}/cars/${carId}/monthly-aggregate`);
  const [{ loading: actionLoading, error: actionError }, action] = useAxios<
    string
  >(
    {},
    {
      manual: true,
    }
  );
  useImperativeHandle(ref, () => ({
    refetch: () => {
      refetch();
    },
  }));
  const onReject = useCallback(
    async (monthlyAggregateId: string) => {
      await action({
        method: 'DELETE',
        url: `/fleets/${fleetId}/cars/${carId}/monthly-aggregate/${monthlyAggregateId}/reject`,
      });
      refetch();
    },
    [fleetId, carId, action, refetch]
  );
  const onAccept = useCallback(
    async (monthlyAggregateId: string) => {
      await action({
        method: 'POST',
        url: `/fleets/${fleetId}/cars/${carId}/monthly-aggregate/${monthlyAggregateId}/accept`,
      });
      refetch();
      monthlyAggregateAccepted();
    },
    [fleetId, carId, action, refetch, monthlyAggregateAccepted]
  );

  return (
    <>
      <ErrorComponent
        loading={loading || actionLoading}
        error={error || actionError}
        refetch={refetch}
      >
        {() => (
          <>
            {!!monthlyAggregates.length && (
              <ListGroup>
                {monthlyAggregates.map((monthlyAggregate) => (
                  <ListGroup.Item
                    key={monthlyAggregate.id}
                    style={{ display: 'flex', alignItems: 'center' }}
                  >
                    <span style={{ width: '80px' }}>
                      {monthlyAggregate.year}/{monthlyAggregate.month}
                    </span>
                    <span style={{ flex: 1 }} />
                    {(monthlyAggregate.accepted === true && 'Accepted') || (
                      <>
                        <Button
                          onClick={() => onAccept(monthlyAggregate.id)}
                          variant="primary"
                          disabled={monthlyAggregate.accepted}
                        >
                          <FontAwesomeIcon icon={faCheck} />
                        </Button>
                        <Button
                          onClick={() => onReject(monthlyAggregate.id)}
                          variant="danger"
                          disabled={!monthlyAggregate.accepted}
                        >
                          <FontAwesomeIcon icon={faTimes} />
                        </Button>
                      </>
                    )}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            )}
            {!monthlyAggregates.length && (
              <div>No monthly aggregate currently for this car.</div>
            )}
          </>
        )}
      </ErrorComponent>
    </>
  );
};
export default forwardRef(MonthlyAggregateList);

import { faCheck } from '@fortawesome/free-solid-svg-icons';
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

export interface PaymentsListHandles {
  refetch: () => void;
}

const PaymentsList: ForwardRefRenderFunction<PaymentsListHandles> = (
  _,
  ref
) => {
  const { fleetId, carId } = useParams<{ fleetId: string; carId: string }>();
  const [{ data: payments, loading, error }, refetch] = useAxios<
    {
      id: string;
      creationTime: string;
      sum: {
        currency: string;
        value: number;
      };
      accepted: boolean;
    }[]
  >(`/fleets/${fleetId}/cars/${carId}/payments`);
  const [{ loading: approveLoading, error: approveError }, approve] = useAxios<
    string
  >(
    {
      method: 'POST',
    },
    {
      manual: true,
    }
  );
  useImperativeHandle<PaymentsListHandles, PaymentsListHandles>(ref, () => ({
    refetch: () => {
      refetch();
    },
  }));
  const onApprove = useCallback(
    async (paymentId: string) => {
      await approve({
        url: `/fleets/${fleetId}/cars/${carId}/payments/${paymentId}/approve`,
      });
      refetch();
    },
    [fleetId, carId, approve, refetch]
  );

  return (
    <>
      <ErrorComponent
        loading={loading || approveLoading}
        error={error || approveError}
        refetch={refetch}
      >
        {() => (
          <>
            {!!payments.length && (
              <ListGroup>
                {payments.map((payment) => (
                  <ListGroup.Item
                    key={payment.id}
                    style={{ display: 'flex', alignItems: 'center' }}
                  >
                    <span style={{ width: '80px', flex: 1 }}>
                      {new Date(payment.creationTime).toLocaleDateString()}{' '}
                      {payment.sum.value} {payment.sum.currency}
                    </span>
                    {(payment.accepted && <span>Approved</span>) || (
                      <Button
                        onClick={() => onApprove(payment.id)}
                        variant="primary"
                        disabled={payment.accepted}
                      >
                        <FontAwesomeIcon icon={faCheck} />
                      </Button>
                    )}
                  </ListGroup.Item>
                ))}
              </ListGroup>
            )}
            {!payments.length && (
              <div>No monthly aggregate currently for this car.</div>
            )}
          </>
        )}
      </ErrorComponent>
    </>
  );
};
export default forwardRef(PaymentsList);

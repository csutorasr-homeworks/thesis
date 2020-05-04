import { Button, Card, Col, Row } from 'react-bootstrap';
import { Link, useParams } from 'react-router-dom';
import ErrorComponent from '../../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export interface CarRowVm {
  id: string;
  licensePlateNumber: string;
  activated: boolean;
  needsToBeServiced: boolean;
}

export default function CarsList() {
  const { fleetId } = useParams();
  const [{ data: fleets, loading, error }, refetch] = useAxios<CarRowVm[]>(
    `/fleets/${fleetId}/cars`
  );
  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <>
          <Row>
            {fleets.map((x) => (
              <Col key={x.id} sm={6} md={4} lg={3} className="mb-4">
                <Link to={`/fleets/${fleetId}/cars/${x.id}`}>
                  <Card>
                    <Card.Body>
                      <Card.Title>{x.licensePlateNumber}</Card.Title>
                      <Button>View</Button>
                    </Card.Body>
                  </Card>
                </Link>
              </Col>
            ))}
          </Row>
          <Row>
            <Col sm={6} md={4} lg={3} className="mb-4">
              <Link to={`/fleets/${fleetId}/cars/new`}>
                <Card>
                  <Card.Body>
                    <Card.Title>Add new car</Card.Title>
                    <Button>New</Button>
                  </Card.Body>
                </Card>
              </Link>
            </Col>
          </Row>
        </>
      )}
    </ErrorComponent>
  );
}

import { Button, Card, Col, Row } from 'react-bootstrap';
import { Link, Redirect } from 'react-router-dom';
import ErrorComponent from '../../Components/Error';
import React from 'react';
import useAxios from 'axios-hooks';

export default function FleetList() {
  const [{ data: fleets, loading, error }, refetch] = useAxios<
    { id: string; name: string }[]
  >('/fleets');
  if (fleets && fleets.length === 1) {
    return <Redirect to={`/fleets/${fleets[0].id}`} />;
  }
  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <>
          <Row>
            {fleets.map((x) => (
              <Col key={x.id} sm={6} md={4} lg={3} className="mb-4">
                <Link to={`/fleets/${x.id}`}>
                  <Card>
                    <Card.Body>
                      <Card.Title>{x.name}</Card.Title>
                      <Button>View</Button>
                    </Card.Body>
                  </Card>
                </Link>
              </Col>
            ))}
          </Row>
          <Row>
            <Col sm={6} md={4} lg={3} className="mb-4">
              <Link to="/fleets/new">
                <Card>
                  <Card.Body>
                    <Card.Title>Add new fleet</Card.Title>
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

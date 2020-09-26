import {
  faChevronRight,
  faPlusSquare,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React from 'react';
import { Button, Card, Col, Row } from 'react-bootstrap';
import { Link, Redirect } from 'react-router-dom';

import ErrorComponent from '../../Components/Error';

export default function FleetList(): JSX.Element {
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
              <Col key={x.id} sm={6} md={4} lg={3} className="mb-4 no-hover">
                <Link to={`/fleets/${x.id}`}>
                  <Card>
                    <Card.Body className="card-right-button">
                      <Card.Title>{x.name}</Card.Title>
                      <Button>
                        <FontAwesomeIcon icon={faChevronRight} />
                      </Button>
                    </Card.Body>
                  </Card>
                </Link>
              </Col>
            ))}
          </Row>
          <Row>
            <Col sm={6} md={4} lg={3} className="mb-4 no-hover ml-auto mr-auto">
              <Link to="/fleets/new">
                <Card>
                  <Card.Body>
                    <Card.Title>Add new fleet</Card.Title>
                    <Button className="card-button">
                      <FontAwesomeIcon icon={faPlusSquare} />
                    </Button>
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

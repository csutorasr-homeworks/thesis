import { faEdit, faEye, faPlusSquare } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import useAxios from 'axios-hooks';
import React from 'react';
import { Button, ButtonGroup, Card, Col, Row } from 'react-bootstrap';
import { Link, useParams } from 'react-router-dom';

import ErrorComponent from '../../../Components/Error';

export interface CarRowVm {
  id: string;
  licensePlateNumber: string;
  activated: boolean;
  needsToBeServiced: boolean;
}

export default function CarsList(): JSX.Element {
  const { fleetId } = useParams<{ fleetId: string }>();
  const [{ data: fleets, loading, error }, refetch] = useAxios<CarRowVm[]>(
    `/fleets/${fleetId}/cars`
  );
  return (
    <ErrorComponent loading={loading} error={error} refetch={refetch}>
      {() => (
        <>
          <Row>
            {fleets.map((x) => (
              <Col key={x.id} sm={6} md={4} lg={3} className="mb-4 no-hover">
                <Link to={`/fleets/${fleetId}/cars/${x.id}`}>
                  <Card>
                    <Card.Body>
                      <Card.Title>{x.licensePlateNumber}</Card.Title>
                      <ButtonGroup className="card-button">
                        <Button>
                          <FontAwesomeIcon icon={faEye} />
                        </Button>
                        <Button
                          variant="secondary"
                          as={Link}
                          to={`/fleets/${fleetId}/cars/${x.id}/edit`}
                        >
                          <FontAwesomeIcon icon={faEdit} />
                        </Button>
                      </ButtonGroup>
                    </Card.Body>
                  </Card>
                </Link>
              </Col>
            ))}
          </Row>
          <Row>
            <Col sm={6} md={4} lg={3} className="mb-4 no-hover">
              <Link to={`/fleets/${fleetId}/cars/new`}>
                <Card>
                  <Card.Body>
                    <Card.Title>Add new car</Card.Title>
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

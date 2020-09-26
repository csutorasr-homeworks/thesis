import './App.css';

import { BrowserRouter, Link, Route, Switch } from 'react-router-dom';
import { Container, Nav, Navbar } from 'react-bootstrap';

import AuthModule from './auth/AuthModule';
import AuthToggle from './auth/AuthToggle';
import Fleets from './Pages/Fleets';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Home from './Pages/Home';
import NotFound from './Pages/NotFound';
import React from 'react';
import { faCar } from '@fortawesome/free-solid-svg-icons';
import { hot } from 'react-hot-loader';

function App() {
  return (
    <BrowserRouter>
      <AuthModule>
        <Navbar bg="dark" expand="lg" variant="dark">
          <Navbar.Brand as={Link} to="/">
            <FontAwesomeIcon icon={faCar} /> Flottapp
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="mr-auto">
              <Nav.Link as={Link} to="/">
                My fleet
              </Nav.Link>
              <Nav.Link as={Link} to="/fleets/new">
                New fleet
              </Nav.Link>
            </Nav>
            <Nav>
              <AuthToggle />
            </Nav>
          </Navbar.Collapse>
        </Navbar>
        <Container className="mt-3">
          <Switch>
            <Route exact path="/">
              <Home />
            </Route>
            <Route path="/fleets">
              <Fleets />
            </Route>
            <Route>
              <NotFound />
            </Route>
          </Switch>
        </Container>
      </AuthModule>
    </BrowserRouter>
  );
}

export default hot(module)(App);

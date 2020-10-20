import './App.css';

import { faCar } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { Container, Nav, Navbar } from 'react-bootstrap';
import { hot } from 'react-hot-loader';
import { BrowserRouter, Link, Route, Switch } from 'react-router-dom';

import AuthGuard from './auth/AuthGuard';
import AuthModule from './auth/AuthModule';
import AuthToggle from './auth/AuthToggle';
import Account from './Pages/Account';
import Fleets from './Pages/Fleets';
import Home from './Pages/Home';
import NotFound from './Pages/NotFound';

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
              <AuthGuard>
                <Home />
              </AuthGuard>
            </Route>
            <Route path="/account">
              <Account />
            </Route>
            <Route path="/fleets">
              <AuthGuard>
                <Fleets />
              </AuthGuard>
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

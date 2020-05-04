import './App.css';

import { BrowserRouter, Link, Route, Switch } from 'react-router-dom';
import { Nav, Navbar } from 'react-bootstrap';

import AuthModule from './auth/AuthModule';
import AuthToggle from './auth/AuthToggle';
import Fleets from './Pages/Fleets';
import Home from './Pages/Home';
import NotFound from './Pages/NotFound';
import React from 'react';
import { hot } from 'react-hot-loader';
import logo from './logo.svg';

function App() {
  return (
    <BrowserRouter>
      <AuthModule>
        <Navbar bg="dark" expand="lg" variant="dark">
          <Navbar.Brand as={Link} to="/">
            <img
              alt=""
              src={logo}
              width="30"
              height="30"
              className="d-inline-block align-top"
            />
            React
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
      </AuthModule>
    </BrowserRouter>
  );
}

export default hot(module)(App);

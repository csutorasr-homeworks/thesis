import './App.css';

import { BrowserRouter, Link, Route, Switch } from 'react-router-dom';

import React from 'react';
import { hot } from 'react-hot-loader';
import logo from './logo.svg';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <Link to="/">home</Link>
          <Link to="/test">test</Link>
          <Switch>
            <Route exact path="/">
              <p>homepage</p>
            </Route>
            <Route path="/test">
              <p>test</p>
            </Route>
          </Switch>
          <p>
            Edit <code>src/App.tsx</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    </BrowserRouter>
  );
}

export default hot(module)(App);

import 'bootstrap/dist/css/bootstrap.css';
import './index.scss';

import * as serviceWorker from './serviceWorker';

import App from './App';
import Axios from 'axios';
import React from 'react';
import ReactDOM from 'react-dom';

import { configure } from 'axios-hooks';

const axios = Axios.create({
  baseURL: '/api',
  headers: {
    Accept: 'application/json',
  },
});

configure({ axios, cache: false });

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();

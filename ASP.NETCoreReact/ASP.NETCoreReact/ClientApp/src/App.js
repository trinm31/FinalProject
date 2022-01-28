import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { UserSession } from './components/UserSession';
import { FetchData } from  './components/FetchData'

import './Index.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
          <Route exact path="/" component={Home} />
          <Route path="/user-session" component={UserSession} />
          <Route path="/weatherforecast" component={FetchData} />
      </Layout>
    );
  }
}

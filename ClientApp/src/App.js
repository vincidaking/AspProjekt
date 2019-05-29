import { Route } from "react-router";
import { Layout } from "./components/Layout";
import React, { Component } from "react";
import { LawPage } from "./components/LawPage";
import { UserPage } from "./components/UserPage";
import { Login } from "./components/Login";
import { RegisterPage } from "./components/RegisterPage";
import { BrowserRouter } from "react-router-dom";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <BrowserRouter>
        <Layout>
          <Route exact path="/" component={UserPage} />

          <Route path="/LawPage" component={LawPage} />
          <Route path="/UserPage" component={UserPage} />

          <Route path="/RegisterPage" component={RegisterPage} />
          <Route path="/Login" component={Login} />
        </Layout>
      </BrowserRouter>
    );
  }
}

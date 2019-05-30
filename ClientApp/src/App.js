import { Router, Route, Link } from "react-router-dom";

import { Layout } from "./components/Layout";
import React, { Component } from "react";
import { LawPage } from "./components/LawPage";
import { LawPageUser } from "./components/LawPageUser";
import { UserPage } from "./components/UserPage";

import { RegisterPage } from "./components/RegisterPage";
import { BrowserRouter } from "react-router-dom";
import { render } from "react-dom";

import history from "./services/history";

import Role from "./services/role";
import PrivateRoute from "./services/PrivateRoute";

import decode from "jwt-decode";
import Jwt from "./helpers/Jwt";

import { authenticationService } from "./services/authentication.service";

import LoginPage from "./components/LoginPage";

export class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      currentUser: null,
      isAdmin: false
    };
  }
  componentDidMount() {
    authenticationService.currentUser.subscribe(x =>
      this.setState({
        currentUser: x,
        isAdmin: x && x.role === Role.Admin
      })
    );
  }
  logout() {
    authenticationService.logout();
    history.push("/login");
  }
  render() {
    const { currentUser, isAdmin } = this.state;
    //console.log(this.state.currentUser);
    return (
      // <BrowserRouter>
      //   <Layout>
      //     <Route exact path="/" component={UserPage} />

      //     <Route path="/LawPage" component={LawPage} />
      //     <Route path="/UserPage" component={UserPage} />

      //     <Route path="/RegisterPage" component={RegisterPage} />
      //   </Layout>
      // </BrowserRouter>
      <Router history={history}>
        <div>
          {currentUser && (
            <nav className="navbar navbar-expand navbar-dark bg-dark">
              <div className="navbar-nav">
                {!isAdmin && (
                  <Link to="/" className="nav-item nav-link">
                    Ustway
                  </Link>
                )}
                {/* {isAdmin && (
                  <Link to="/lawpage" className="nav-item nav-link">
                    Ustway
                  </Link>)} */}

                <Link to="/lawpage" className="nav-item nav-link">
                  Ustway
                </Link>

                {isAdmin && (
                  <Link to="/userpage" className="nav-item nav-link">
                    Użytkownicy
                  </Link>
                )}
                <a onClick={this.logout} className="nav-item nav-link">
                  Wylogowanie
                </a>
              </div>
            </nav>
          )}
          <div className="jumbotron">
            <div className="container">
              <div className="row">
                <div className="col-md-6 offset-md-3">
                  <PrivateRoute exact path="/" component={LawPageUser} />

                  {/* <PrivateRoute exact path="/lawpage" component={LawPageUser} roles={[Role.Admin]} /> */}

                  <PrivateRoute
                    path="/userpage"
                    roles={[Role.Admin]}
                    component={UserPage}
                  />
                  <PrivateRoute path="/registerpage" component={RegisterPage} />
                  <PrivateRoute path="/lawpage" component={LawPage} />

                  {/* <PrivateRoute exact
                    path="/lawpage"
                    roles={[Role.Admin]}
                    component={LawPage}
                  /> */}
                  <Route path="/login" component={LoginPage} />
                </div>
              </div>
            </div>
          </div>
        </div>
      </Router>
    );
  }
}
export default App;

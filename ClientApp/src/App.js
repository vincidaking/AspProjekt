import { Router, Route, Link } from "react-router-dom";

import { Layout } from "./components/Layout";
import React, { Component } from "react";
import { LawPage } from "./components/LawPage";
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
    console.log(this.state.currentUser);
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
                <Link to="/" className="nav-item nav-link">
                  Res List
                </Link>
                {isAdmin && (
                  <Link to="/registerpage" className="nav-item nav-link">
                    Add User
                  </Link>
                )}
                {isAdmin && (
                  <Link to="/userpage" className="nav-item nav-link">
                    User List
                  </Link>
                )}
                <a onClick={this.logout} className="nav-item nav-link">
                  Logout
                </a>
              </div>
            </nav>
          )}
          <div className="jumbotron">
            <div className="container">
              <div className="row">
                <div className="col-md-6 offset-md-3">
                  <PrivateRoute exact path="/" component={LawPage} />
                  <PrivateRoute
                    path="/userpage"
                    roles={[Role.Admin]}
                    component={UserPage}
                  />
                  <PrivateRoute
                    path="/registerpage"
                    roles={[Role.Admin]}
                    component={RegisterPage}
                  />
                  {/* <PrivateRoute
                    path="/edituser/:id"
                    roles={[Role.Admin]}
                    component={EditUser}
                  />
                  <PrivateRoute
                    path="/deluser/:id"
                    roles={[Role.Admin]}
                    component={DelUser}
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

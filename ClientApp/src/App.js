import { Router, Route, Link } from "react-router-dom";
import React, { Component } from "react";
import { LawPage } from "./components/LawPage";
import { LawPageUser } from "./components/LawPageUser";
import { UserPage } from "./components/UserPage";
import history from "./services/history";
import Role from "./services/role";
import PrivateRoute from "./services/PrivateRoute";
import { authenticationService } from "./services/authentication.service";
import LoginPage from "./components/LoginPage";

import { Row, Jumbotron, Col } from "reactstrap";

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
                {isAdmin && (
                  <Link to="/lawpage" className="nav-item nav-link">
                    Ustway
                  </Link>
                )}

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
          <Jumbotron>
            <Row>
              <Col xd="3" />
              <Col xs="auto">
                <PrivateRoute exact path="/" component={LawPageUser} />

                {/* <PrivateRoute exact path="/lawpage" component={LawPageUser} roles={[Role.Admin]} /> */}

                <PrivateRoute
                  path="/userpage"
                  roles={[Role.Admin]}
                  component={UserPage}
                />

                <PrivateRoute
                  exact
                  path="/lawpage"
                  roles={[Role.Admin]}
                  component={LawPage}
                />
                <Route path="/login" component={LoginPage} />
              </Col>
              <Col xd="3" />
            </Row>
          </Jumbotron>
        </div>
      </Router>
    );
  }
}
export default App;

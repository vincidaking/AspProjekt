import React, { Component } from "react";
import "./Login.css";
import AuthService from "./AuthService";
import { FormGroup, Input, Label } from "reactstrap";

export class Login extends Component {
  constructor() {
    super();
    this.handleChange = this.handleChange.bind(this);
    this.handleFormSubmit = this.handleFormSubmit.bind(this);
    this.Auth = new AuthService();
  }
  render() {
    return (
      <div className="center">
        <div className="card">
          <FormGroup className="my-3">
            <Label>Login</Label>

            <Input
              className="form-item"
              placeholder="Podaj login"
              name="username"
              type="text"
              onChange={this.handleChange}
            />
            <Input
              className="form-item"
              placeholder="Podaj hasło"
              name="password"
              type="password"
              onChange={this.handleChange}
            />
            <Input className="form-submit" value="Loguj" type="submit" />
          </FormGroup>
        </div>
      </div>
    );
  }

  handleChange(e) {
    this.setState({
      [e.target.name]: e.target.value
    });
  }

  handleFormSubmit(e) {
    e.preventDefault();

    this.Auth.login(this.state.username, this.state.password)
      .then(res => {
        this.props.history.replace("/UserPage");
      })
      .catch(err => {
        alert(err);
      });
  }

  componentWillMount() {
    if (this.Auth.loggedIn()) this.props.history.replace("/UserPage");
  }
}

export default Login;

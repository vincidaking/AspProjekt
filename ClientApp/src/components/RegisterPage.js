import React, { Component } from "react";
import axios from "../helpers/axios.api"
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Input,
  Label
} from "reactstrap";

export class RegisterPage extends Component {
  state = {
    users: [],
    newUserData: {
      firstName: "",
      lastName: "",
      username: "",
      password: ""
      
    },
    editUserData: {
      id: "",
      firstName: "",
      lastName: "",
      username: "",
      password: ""
      
    },
    newUserModal: false,
    editUserModal: false
  };

  toggleNewUserModel() {
    this.setState({
      newUserModal: !this.state.newUserModal
    });
  }

  addUser() {
    axios
      .post("users/registred", this.state.newUserData)
      .then(response => {
        let { users } = this.state;

        users.push(response.data);

        this.setState({
          users,
          newUserModal: false,
          newUserData: {
            firstName: "",
            lastName: "",
            username: "",
            password: ""
            
          }
        });
      });
  }

  render() {
    return (
      <div className="App container">
        <Button
          className="my-3"
          color="primary"
          onClick={this.toggleNewUserModel.bind(this)}
        >
          Rejestracja{" "}
        </Button>
        <Modal
          isOpen={this.state.newUserModal}
          toggle={this.toggleNewUserModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewUserModel.bind(this)}>
            Rejestracja
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="firstName">Imie</Label>
              <Input
                id="firstName"
                placeholder="Podaj Imie"
                value={this.state.newUserData.firstName}
                onChange={e => {
                  let { newUserData } = this.state;
                  newUserData.firstName = e.target.value;
                  this.setState({ newUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lastname">Nazwisko</Label>
              <Input
                id="lastname"
                placeholder="Podaj Nazwisko"
                value={this.state.newUserData.lastName}
                onChange={e => {
                  let { newUserData } = this.state;
                  newUserData.lastName = e.target.value;
                  this.setState({ newUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="username">Email</Label>
              <Input
                id="username"
                placeholder="Podaj Emiala"
                value={this.state.newUserData.username}
                onChange={e => {
                  let { newUserData } = this.state;
                  newUserData.username = e.target.value;
                  this.setState({ newUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="password">Password</Label>
              <Input
                type="password"
                id="firsname"
                placeholder="Podaj Hasło"
                value={this.state.newUserData.password}
                onChange={e => {
                  let { newUserData } = this.state;
                  newUserData.password = e.target.value;
                  this.setState({ newUserData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addUser.bind(this)}>
              Dodaj
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleNewUserModel.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>
      </div>
    );
  }
}
export default RegisterPage;

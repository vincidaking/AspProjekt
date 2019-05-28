import React, { Component } from "react";
import axios from "axios";
import {
  Table,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Input,
  Label
} from "reactstrap";

export class UserPage extends Component {
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

  // componetWillMount() {
  //   axios.get("http://localhost:57548/users/").then(response => {
  //     this.setState({
  //       users: response.data
  //     });
  //   });
  // }

  componentWillMount() {
    this._refreshUser();
  }

  toggleNewUserModel() {
    this.setState({
      newUserModal: !this.state.newUserModal
    });
  }

  toggleEditUserModel() {
    this.setState({
      editUserModal: false
    });
  }

  addUser() {
    axios
      .post("http://localhost:57548/users/registred", this.state.newUserData)
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

  updateUser() {
    let { firstName, lastName, username, password } = this.state.editUserData;
    axios
      .put("http://localhost:57548/users/" + this.state.editUserData.id, {
        firstName,
        lastName,
        username,
        password
      })
      .then(response => {
        this._refreshUser();
      });
  }

  editUser(id, firstName, lastName, username, password) {
    if (password == null) password = "";
    this.setState({
      editUserData: { id, firstName, lastName, username, password },
      editUserModel: true
    });
  }

  deleteUser(id) {
    axios.delete("http://localhost:57548/users" + id).then(response => {
      this._refreshUser();
    });
  }

  _refreshUser() {
    // axios.get(`http://localhost:57548/users/`).then(res => {
    //   const users = res.data;
    //   this.setState({ users });
    // });
    axios.get("http://localhost:57548/users/").then(response => {
      this.setState({
        users: response.data
      });
    });
  }

  render() {
    let users = this.state.users.map(user => {
      return (
        <tr key={user.id}>
          <td>{user.firstName}</td>
          <td>{user.lastName}</td>
          <td>{user.username}</td>
          <td>
            <Button
              color="success"
              size="sm"
              className="mr-2"
              onClick={this.editUser.bind(
                this,
                user.id,
                user.firstName,
                user.lastName,
                user.username,
                user.password
              )}
            >
              Edit
            </Button>
            <Button
              color="danger"
              size="sm"
              onClick={this.deleteUser.bind(this, user.id)}
            >
              Delete
            </Button>
          </td>
        </tr>
      );
    });
    return (
      <div className="App container">
        <h1>Dodanie Uzytkownika</h1>
        <Button
          className="my-3"
          color="primary"
          onClick={this.toggleNewUserModel.bind(this)}
        >
          Dodanie Uzytkownika
        </Button>
        <Modal
          isOpen={this.state.newUserModal}
          toggle={this.toggleNewUserModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewUserModel.bind(this)}>
            Dodanie nowego użytkownika
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
        <Modal isOpen={this.state.editUserModel}>
          <ModalHeader>Edytowanie Uzytkownika</ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="firstName">Imie</Label>
              <Input
                id="firstName"
                value={this.state.editUserData.firstName}
                onChange={e => {
                  let { editUserData } = this.state;
                  editUserData.firstName = e.target.value;
                  this.setState({ editUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lastname">Nazwisko</Label>
              <Input
                id="lastname"
                value={this.state.editUserData.lastName}
                onChange={e => {
                  let { editUserData } = this.state;
                  editUserData.lastName = e.target.value;
                  this.setState({ editUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="username">Email</Label>
              <Input
                id="username"
                value={this.state.editUserData.username}
                onChange={e => {
                  let { editUserData } = this.state;
                  editUserData.username = e.target.value;
                  this.setState({ editUserData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="password">Password</Label>
              <Input
                type="password"
                id="firsname"
                value={this.state.editUserData.password}
                onChange={e => {
                  let { editUserData } = this.state;
                  editUserData.password = e.target.value;
                  this.setState({ editUserData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.updateUser.bind(this)}>
              Zapisz
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleEditUserModel.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Table>
          <thead>
            <tr>
              <th>Imie</th>
              <th>Nazwisko</th>
              <th>Login</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>{users}</tbody>
        </Table>
      </div>
    );
  }
}
export default UserPage;

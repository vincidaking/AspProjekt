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
  constructor() {
    super();

    this.toggleEditUserModel = this.toggleEditUserModel.bind(this);
  }

  state = {
    users: [],
    newUserData: {
      firstName: "",
      lastName: "",
      username: "",
      password: "",
      role: ""
    },
    editUserData: {
      id: "",
      firstName: "",
      lastName: "",
      username: "",
      password: "",
      role: "user"
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

  toggleEditUserModel() {
    this.setState({
      editUserModal: !this.state.editUserModal
    });
  }

  updateUser() {
    let {
      id,
      firstName,
      lastName,
      username,
      password,
      role
    } = this.state.editUserData;
    axios
      .put("http://localhost:57548/users/" + this.state.editUserData.id, {
        id,
        firstName,
        lastName,
        username,
        password,
        role
      })
      .then(response => {
        this._refreshUser();
      });
  }

  editUser(id, firstName, lastName, username, password, role) {
    if (password == null) password = "";
    this.setState({
      editUserData: { id, firstName, lastName, username, password, role },
      editUserModel: true
    });
  }

  deleteUser(id) {
    axios.delete("http://localhost:57548/users/" + id).then(response => {
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
              Edytuj
            </Button>
            <Button
              color="danger"
              size="sm"
              onClick={this.deleteUser.bind(this, user.id)}
            >
              Usun
            </Button>
          </td>
        </tr>
      );
    });
    return (
      <div className="App container">
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
            <Button color="secondary" onClick={this.toggleEditUserModel}>
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
              <th>Akcja</th>
            </tr>
          </thead>
          <tbody>{users}</tbody>
        </Table>
      </div>
    );
  }
}
export default UserPage;

import React, { Component } from "react";

import { FormGroup, Input, Label } from "reactstrap";

import axios from "../helpers/axios.api";

import {
  MDBTable,
  MDBTableBody,
  MDBTableHead,
  MDBBtn,
  MDBModal,
  MDBModalBody,
  MDBModalHeader,
  MDBModalFooter
} from "mdbreact";

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
    editUserModel: false
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
      editUserModel: !this.state.editUserModel
    });
  }

  updateUser() {
    let {
      id,
      firstName,
      lastName,
      username,
      password
    } = this.state.editUserData;
    axios
      .put("users/" + this.state.editUserData.id, {
        id,
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
    axios.delete("users/" + id).then(response => {
      this._refreshUser();
    });
  }

  _refreshUser() {
    // axios.get(`http://localhost:57548/users/`).then(res => {
    //   const users = res.data;
    //   this.setState({ users });
    // });
    axios.get("users/").then(response => {
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
            <MDBBtn
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
            </MDBBtn>{" "}
            <MDBBtn
              color="danger"
              size="sm"
              onClick={this.deleteUser.bind(this, user.id)}
            >
              Usun
            </MDBBtn>
          </td>
        </tr>
      );
    });
    return (
      <div className="App container">
        <MDBModal isOpen={this.state.editUserModel} fullHeight position="right">
          <MDBModalHeader>Edytowanie Uzytkownika</MDBModalHeader>
          <MDBModalBody>
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
          </MDBModalBody>
          <MDBModalFooter>
            <MDBBtn color="primary" onClick={this.updateUser.bind(this)}>
              Zapisz
            </MDBBtn>{" "}
            <MDBBtn color="secondary" onClick={this.toggleEditUserModel}>
              Cancel
            </MDBBtn>
          </MDBModalFooter>
        </MDBModal>

        <MDBTable striped>
          <MDBTableHead>
            <tr>
              <th>Imie</th>
              <th>Nazwisko</th>
              <th>Login</th>
              <th>Akcja</th>
            </tr>
          </MDBTableHead>
          <MDBTableBody>{users}</MDBTableBody>
        </MDBTable>
      </div>
    );
  }
}
export default UserPage;

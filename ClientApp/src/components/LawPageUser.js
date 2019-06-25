import React, { Component } from "react";
import axios from "../helpers/axios.api";
import {
  Table,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Label
} from "reactstrap";

import { authenticationService } from "../services/authentication.service";

export class LawPageUser extends Component {
  state = {
    laws: [],
    newVoteData: {
      id: "",
      username: "",
      voteTypeId: ""
    },
    newVoteModel: false
  };

  componentWillMount() {
    this._refreshLaw();
  }

  toggleNewVoteModel(id) {
    this.setState({
      newVoteModel: !this.state.newVoteModel,
      newVoteData: { id }
    });
  }

  _refreshLaw() {
    axios
      .get(
        "api/Votes/withOptionVote/" +
          authenticationService.currentUserValue.username
      )
      .then(response => {
        this.setState({
          laws: response.data
        });
      });
  }

  refreshPage() {
    window.location.reload();
  }

  addVote() {
    axios.post("api/Votes/", this.state.newVoteData).then(response => {
      let { laws } = this.state;
      laws.push(response.data);
      this.setState({
        newVoteModel: false,
        newVoteData: {
          id: "",
          voteTypeId: ""
        }
      });
      this._refreshLaw();
    });
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.name}</td>
          <td>{law.lawText}</td>
          <td>{law.dateAdd}</td>
          <td>{law.dateEnd}</td>
          <td>
            <Button
              className="my-3"
              color="primary"
              onClick={this.toggleNewVoteModel.bind(this, law.id)}
            >
              Głosuj
            </Button>
          </td>
        </tr>
      );
    });

    return (
      <div className="App container">
        <Modal
          isOpen={this.state.newVoteModel}
          toggle={this.toggleNewVoteModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewVoteModel.bind(this)}>
            Zagłosuj
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="voteTypeId">Typ głosu</Label>
              <select
                className="form-control"
                data-style="form-control"
                title="Typ głosu"
                onChange={e => {
                  let { newVoteData } = this.state;
                  newVoteData.voteTypeId = e.target.value;
                  newVoteData.username =
                    authenticationService.currentUserValue.username;
                  this.setState({ newVoteData });
                }}
              >
                <option hidden>Głusujesz na:</option>
                <option value="0">Zgadzam </option>
                <option value="1">Nie zgadzam</option>
                <option value="2">Wstrzymuje się</option>
              </select>
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addVote.bind(this)}>
              Głosuj
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleNewVoteModel.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Table>
          <thead>
            <tr>
              <th>Nazwa</th>

              <th>Tresc</th>
              <th>Data Dodania</th>
              <th>Data Konca</th>
              <th>Akcja</th>
            </tr>
          </thead>
          <tbody>{laws}</tbody>
        </Table>
      </div>
    );
  }
}
export default LawPageUser;

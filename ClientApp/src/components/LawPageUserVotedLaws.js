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

export class LawPageUserVotedLaws extends Component {
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
        "api/Laws/withoutOptionVote/" +
          authenticationService.currentUserValue.username
      )
      .then(response => {
        this.setState({
          laws: response.data
        });
      });
  }

  addVote() {
    axios.post("api/Votes/", this.state.newVoteData).then(response => {
      this.setState({
        newVoteModel: false,
        newVoteData: {
          id: "",
          voteTypeId: ""
        }
      });
    });

    //this._refreshLaw();
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.name}</td>
          <td>{law.lawText}</td>
          <td>{law.dateAdd}</td>
          <td>{law.dateEnd}</td>
        </tr>
      );
    });

    return (
      <div className="App container">
        <Table>
          <thead>
            <tr>
              <th>Nazwa</th>

              <th>Tresc</th>
              <th>Data Dodania</th>
              <th>Data Konca</th>
            </tr>
          </thead>
          <tbody>{laws}</tbody>
        </Table>
      </div>
    );
  }
}
export default LawPageUserVotedLaws;

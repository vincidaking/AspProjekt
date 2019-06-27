import React, { Component } from "react";
import axios from "../helpers/axios.api";

import moment from "moment";

import { authenticationService } from "../services/authentication.service";
import { MDBTable, MDBTableBody, MDBTableHead } from "mdbreact";
export class LawPageUserVotedLaws extends Component {
  state = {
    laws: []
  };

  componentWillMount() {
    this._refreshLaw();
  }
  renderSwitch(param) {
    switch (param) {
      case 0:
        return "Tak";
      case 1:
        return "Nie";
      case 2:
        return "Nie wiem";
      default:
        return "foo";
    }
  }
  _refreshLaw() {
    axios
      .get(
        "api/Votes/withoutOptionVote/" +
          authenticationService.currentUserValue.username
      )
      .then(response => {
        this.setState({
          laws: response.data
        });
      });
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.name}</td>
          <td>{law.lawText}</td>
          <td>{moment(law.dateAdd).format("MM/DD/YYYY")}</td>
          <td>{moment(law.dateEnd).format("MM/DD/YYYY")}</td>
          <td>{this.renderSwitch(law.voteType)}</td>
        </tr>
      );
    });

    return (
      <div className="App container">
        <MDBTable striped>
          <MDBTableHead>
            <tr>
              <th>Nazwa</th>

              <th>Tresc</th>
              <th>Data Dodania</th>
              <th>Data Konca</th>
              <th>Głos</th>
            </tr>
          </MDBTableHead>
          <MDBTableBody>{laws}</MDBTableBody>
        </MDBTable>
      </div>
    );
  }
}
export default LawPageUserVotedLaws;

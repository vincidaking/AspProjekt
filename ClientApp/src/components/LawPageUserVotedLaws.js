import React, { Component } from "react";
import axios from "../helpers/axios.api";
import { Table } from "reactstrap";

import { authenticationService } from "../services/authentication.service";

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
          <td>{law.dateAdd}</td>
          <td>{law.dateEnd}</td>
          <td>{this.renderSwitch(law.voteType)}</td>
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
              <th>Głos</th>
            </tr>
          </thead>
          <tbody>{laws}</tbody>
        </Table>
      </div>
    );
  }
}
export default LawPageUserVotedLaws;

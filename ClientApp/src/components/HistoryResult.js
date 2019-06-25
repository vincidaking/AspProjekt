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

import authenticationService from "../services/authentication.service";

export class HistoryResult extends Component {
  state = {
    laws: [],
    authenticationService
  };

  componentWillMount() {
    this._refreshLaw();
  }

  _refreshLaw() {
    axios.get("api/Votes/HistoryResult/").then(response => {
      this.setState({
        laws: response.data
      });
    });
  }

  renderSwitch(param) {
    switch (param) {
      case 0:
        return "Wdrożenie";
      case 1:
        return "Nie wdrażamy";
      case 2:
        return "Ponowne Głosowanie";
      default:
        return "foo";
    }
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.name}</td>
          <td>{law.lawText}</td>
          <td>{law.dateEnd}</td>
          <td>{law.accept}</td>
          <td>{law.decline}</td>
          <td>{law.none}</td>
          <td> {this.renderSwitch(law.winer)}</td>
          <td />
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

              <th>Data Konca</th>
              <th>Zgadzam</th>
              <th>Nie zgadzam</th>
              <th>Wstrzymuje się</th>
              <th>Werdykt</th>
            </tr>
          </thead>
          <tbody>{laws}</tbody>
        </Table>
      </div>
    );
  }
}
export default HistoryResult;

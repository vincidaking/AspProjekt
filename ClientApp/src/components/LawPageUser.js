import React, { Component } from "react";
import axios from "../helpers/axios.api";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Label
} from "reactstrap";

import { authenticationService } from "../services/authentication.service";

import { PDFDownloadLink } from "@react-pdf/renderer";
import { MDBTable, MDBTableBody, MDBTableHead } from "mdbreact";
import PDF from "../services/PDF";

import moment from "moment";

export class LawPageUser extends Component {
  constructor() {
    super();

    this.toggleDataModel = this.toggleDataModel.bind(this);
  }
  state = {
    laws: [],
    newVoteData: {
      id: "",
      username: "",
      voteTypeId: ""
    },
    LawsData: {
      id: "",
      name: "",
      lawText: "",
      dateAdd: "",
      dateEnd: ""
    },
    newVoteModel: false,
    dataModel: false
  };

  componentWillMount() {
    this._refreshLaw();
  }

  toggleDataModel() {
    this.setState({
      dataModel: !this.state.dataModel
    });
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

  addVote() {
    axios.post("api/Votes/", this.state.newVoteData).then(response => {
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

  dataLaws(id, name, lawText, dateAdd, dateEnd) {
    this.setState({
      LawsData: { id, name, lawText, dateAdd, dateEnd },
      dataModel: true
    });
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.name}</td>
          {/* <td>{law.lawText}</td> */}
          <td>{moment(law.dateAdd).format("MM/DD/YYYY")}</td>

          <td>{moment(law.dateEnd).format("MM/DD/YYYY")}</td>
          <td>
            <Button
              className="my-3"
              color="primary"
              onClick={this.toggleNewVoteModel.bind(this, law.id)}
            >
              Głosuj
            </Button>{" "}
            <Button
              color="info"
              onClick={this.dataLaws.bind(
                this,
                law.id,
                law.name,
                law.lawText,
                law.dateAdd,
                law.dateEnd
              )}
            >
              Szczegóły
            </Button>{" "}
            <Button color="light">
              <PDFDownloadLink
                id={law.id}
                document={<PDF id={law.id} />}
                fileName={law.name}
              >
                {({ blob, url, loading, error }) =>
                  loading ? "Loading document..." : "PDF"
                }
              </PDFDownloadLink>
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

        <Modal isOpen={this.state.dataModel}>
          <ModalHeader>Nazwa ustawy: {this.state.LawsData.name}</ModalHeader>
          <ModalBody>
            Treść: <text>{this.state.LawsData.lawText}</text>
            <div />
            <div>
              Data Dodania:
              <div>
                {" "}
                {moment(this.state.LawsData.dateAdd).format("MM/DD/YYYY LTS")}
              </div>
              Data Konca:
              <div>
                {" "}
                {moment(this.state.LawsData.dateEnd).format("MM/DD/YYYY LTS")}
              </div>
            </div>
          </ModalBody>
          <ModalFooter>
            <Button color="secondary" onClick={this.toggleDataModel}>
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <MDBTable striped>
          <MDBTableHead>
            <tr>
              <th>Nazwa</th>

              {/* <th>Tresc</th> */}
              <th>Data Dodania</th>
              <th>Data Konca</th>
              <th>Akcja</th>
            </tr>
          </MDBTableHead>
          <MDBTableBody>{laws}</MDBTableBody>
        </MDBTable>
      </div>
    );
  }
}
export default LawPageUser;

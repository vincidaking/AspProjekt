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

export class LawPage extends Component {
  state = {
    laws: [],
    newLawsData: {
      LawText: "",
      DateAdd: "",
      DateEnd: ""
    },
    editLawsData: {
      id: "",
      LawText: "",
      DateAdd: "",
      DateEnd: ""
    },
    newLawsModal: false,
    editLawsModal: false
  };

  // componetWillMount() {
  //   axios.get("http://localhost:57548/laws/").then(response => {
  //     this.setState({
  //       laws: response.data
  //     });
  //   });
  // }

  componentWillMount() {
    this._refreshLaw();
  }

  toggleNewLawsModel() {
    this.setState({
      newLawsModal: !this.state.newLawsModal
    });
  }

  toggleEditLawsModel() {
    this.setState({
      editLawsModal: !this.state.editLawsModal
    });
  }

  addLaw() {
    axios
      .post("http://localhost:57548/api/laws/", this.state.newLawsData)
      .then(response => {
        let { laws } = this.state;

        laws.push(response.data);

        this.setState({
          laws,
          newLawsModal: false,
          newLawsData: {
            LawText: "",
            DateAdd: "",
            DateEnd: ""
          }
        });
      });
  }

  updateLaws() {
    let { LawText, DateAdd, DateEnd } = this.state.editLawsData;
    axios
      .put("http://localhost:57548/api/laws/" + this.state.editLawsData.id, {
        LawText,
        DateAdd,
        DateEnd
      })
      .then(response => {
        this._refreshLaw();
      });
  }

  editLaws(id, LawText, DateAdd, DateEnd) {
    this.setState({
      editLawsData: { id, LawText, DateAdd, DateEnd },
      editLawsModel: !this.state.editLawsModal
    });
  }

  deleteLaw(id) {
    axios.delete("http://localhost:57548/api/laws/" + id).then(response => {
      this._refreshLaw();
    });
  }

  _refreshLaw() {
    // axios.get(`http://localhost:57548/laws/`).then(res => {
    //   const laws = res.data;
    //   this.setState({ laws });
    // });
    axios.get("http://localhost:57548/api/laws/").then(response => {
      this.setState({
        laws: response.data
      });
    });
  }

  render() {
    let laws = this.state.laws.map(law => {
      return (
        <tr key={law.id}>
          <td>{law.LawText}</td>
          <td>{law.DateAdd}</td>
          <td>{law.DateEnd}</td>
          <td>
            <Button
              color="success"
              size="sm"
              className="mr-2"
              onClick={this.editLaws.bind(
                this,
                law.id,
                law.LawText,
                law.DateAdd,
                law.DateEnd
              )}
            >
              Edit
            </Button>
            <Button
              color="danger"
              size="sm"
              onClick={this.deleteLaw.bind(this, law.id)}
            >
              Delete
            </Button>
          </td>
        </tr>
      );
    });
    return (
      <div className="App container">
        <h1>Dodanie Uchwały</h1>
        <Button
          className="my-3"
          color="primary"
          onClick={this.toggleNewLawsModel.bind(this)}
        >
          Dodanie Uchwały
        </Button>
        <Modal
          isOpen={this.state.newLawsModal}
          toggle={this.toggleNewLawsModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewLawsModel.bind(this)}>
            Dodanie nowej uchwaly
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="LawText">Tresc</Label>
              <Input
                id="LawText"
                placeholder="Podaj Tresc"
                value={this.state.newLawsData.LawText}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.LawText = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="DateAdd">Data Dodania</Label>
              <Input
                id="DateAdd"
                placeholder="Podaj Date"
                value={this.state.newLawsData.DateAdd}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.DateAdd = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="DateEnd">Data Konca</Label>
              <Input
                id="DateEnd"
                placeholder="Podaj Date"
                value={this.state.newLawsData.DateEnd}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.DateEnd = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addLaw.bind(this)}>
              Dodaj
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleNewLawsModel.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>
        <Modal
          isOpen={this.state.editLawsModal}
          toggle={this.toggleEditLawsModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleEditLawsModel.bind(this)}>
            Edytowanie Uchwały
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="LawText">Tresc</Label>
              <Input
                id="LawText"
                value={this.state.editLawsData.LawText}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.LawText = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="DateAdd">Data</Label>
              <Input
                id="DateAdd"
                value={this.state.editLawsData.DateAdd}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.DateAdd = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="DateEnd">Data</Label>
              <Input
                id="DateEnd"
                value={this.state.editLawsData.DateEnd}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.DateEnd = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.updateLaws.bind(this)}>
              Edycja
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleEditLawsModel.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Table>
          <thead>
            <tr>
              <th>Tresc</th>
              <th>Data Dodania</th>
              <th>Data Konca</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>{laws}</tbody>
        </Table>
      </div>
    );
  }
}
export default LawPage;

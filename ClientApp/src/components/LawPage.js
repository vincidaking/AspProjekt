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
  Input,
  Label
} from "reactstrap";

import * as Yup from "yup";
import { Formik, Field, Form, ErrorMessage } from "formik";

export class LawPage extends Component {
  constructor() {
    super();

    this.toggleEditLawsModel = this.toggleEditLawsModel.bind(this);
  }
  state = {
    laws: [],
    newLawsData: {
      name: "",
      lawText: "",
      dateAdd: "",
      dateEnd: ""
    },
    editLawsData: {
      id: "",
      name: "",
      lawText: "",
      dateAdd: "",
      dateEnd: ""
    },
    newLawsModel: false,
    editLawsModel: false
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
      newLawsModel: !this.state.newLawsModel
    });
  }

  toggleEditLawsModel() {
    this.setState({
      // editLawsModel: false

      editLawsModel: !this.state.editLawsModel
    });
  }

  addLaw() {
    axios.post("api/Laws/", this.state.newLawsData).then(response => {
      let { laws } = this.state;

      laws.push(response.data);

      this.setState({
        laws,
        newLawsModel: false,
        newLawsData: {
          name: "",
          lawText: "",
          dateAdd: "",
          dateEnd: ""
        }
      });

      //this._refreshLaw();
    });
  }

  updateLaws() {
    let { id, name, lawText, dateAdd, dateEnd } = this.state.editLawsData;
    axios
      .put("api/Laws/" + this.state.editLawsData.id, {
        id,
        name,
        lawText,
        dateAdd,
        dateEnd
      })
      .then(response => {
        this._refreshLaw();
      });
  }

  editLaws(id, name, lawText, dateAdd, dateEnd) {
    this.setState({
      editLawsData: { id, name, lawText, dateAdd, dateEnd },
      editLawsModel: true
    });
  }

  deleteLaw(id) {
    axios.delete("api/Laws/" + id).then(response => {
      this._refreshLaw();
    });
  }

  _refreshLaw() {
    // axios.get(`http://localhost:57548/laws/`).then(res => {
    //   const laws = res.data;
    //   this.setState({ laws });
    // });
    axios.get("api/Laws/").then(response => {
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
          <td>
            <Button
              color="success"
              size="sm"
              className="mr-2"
              onClick={this.editLaws.bind(
                this,
                law.id,
                law.name,
                law.lawText,
                law.dateAdd,
                law.dateEnd
              )}
            >
              Edytuj
            </Button>
            <Button
              color="danger"
              size="sm"
              onClick={this.deleteLaw.bind(this, law.id)}
            >
              Usun
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
          Dodaj
        </Button>
        <Modal
          isOpen={this.state.newLawsModel}
          toggle={this.toggleNewLawsModel.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewLawsModel.bind(this)}>
            Dodanie nowej uchwaly
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="name">Nazwa</Label>
              <Input
                id="name"
                placeholder="Podaj Nazwe"
                value={this.state.newLawsData.name}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.name = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lawText">Tresc</Label>
              <Input
                id="lawText"
                placeholder="Podaj Tresc"
                value={this.state.newLawsData.lawText}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.lawText = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="dateAdd">Data Dodania</Label>
              <Input
                id="dateAdd"
                type="date"
                placeholder="Podaj Date"
                value={this.state.newLawsData.dateAdd}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.dateAdd = e.target.value;
                  this.setState({ newLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="dateEnd">Data Konca</Label>
              <Input
                id="dateEnd"
                type="date"
                placeholder="Podaj Date"
                value={this.state.newLawsData.dateEnd}
                onChange={e => {
                  let { newLawsData } = this.state;
                  newLawsData.dateEnd = e.target.value;
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

        <Modal isOpen={this.state.editLawsModel}>
          <ModalHeader>Edytowanie Uchwały</ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="name">Nazwa</Label>
              <Input
                id="name"
                value={this.state.editLawsData.name}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.name = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="lawText">Tresc</Label>
              <Input
                id="lawText"
                value={this.state.editLawsData.lawText}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.lawText = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
            {/* <FormGroup>
              <Label for="dateAdd">Data</Label>
              <Input
                id="dateAdd"
                type="date"
                value={this.state.editLawsData.dateAdd}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.dateAdd = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup> */}
            <FormGroup>
              <Label for="dateEnd">Data</Label>
              <Input
                id="dateEnd"
                type="date"
                value={this.state.editLawsData.dateEnd}
                onChange={e => {
                  let { editLawsData } = this.state;
                  editLawsData.dateEnd = e.target.value;
                  this.setState({ editLawsData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.updateLaws.bind(this)}>
              Edycja
            </Button>{" "}
            <Button color="secondary" onClick={this.toggleEditLawsModel}>
              Cancel
            </Button>
          </ModalFooter>
        </Modal>

        <Table responsive>
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
export default LawPage;

import React, { Component } from "react";
import axios from "../helpers/axios.api"
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

export class LawPageUser extends Component {
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

  

  

 

   

  

  



  _refreshLaw() {
    // axios.get(`http://localhost:57548/laws/`).then(res => {
    //   const laws = res.data;
    //   this.setState({ laws });
    // });
    axios.get("api/Laws").then(response => {
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

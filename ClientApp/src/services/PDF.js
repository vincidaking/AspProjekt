import React, { Component } from "react";
import source from "../helpers/Fonts/Calibri/calibri.ttf";

import api from "../helpers/axios.api";

import {
  Page,
  Text,
  View,
  Document,
  StyleSheet,
  Font
} from "@react-pdf/renderer";

Font.register({ family: "Calibri", src: source });

// Create styles
const styles = StyleSheet.create({
  page: {
    backgroundColor: "white",
    //fontFamily: "Calibri",

    padding: "2cm"
  },
  header: {
    textAlign: "center"
  },
  headerlow: {
    textAlign: "center",
    fontSize: 13,
    padding: 10
  },

  lawName: {
    textAlign: "left",

    fontSize: 13
  },

  lawCount: {
    marginHorizontal: "2cm",
    fontSize: 12,
    textAlign: "justify"
  },
  textName: {
    textAlign: "left",
    fontSize: 11,
    padding: 10
  },

  signup: {
    textAlign: "right",
    fontSize: 14,
    padding: 15
  },

  body: {
    padding: 10
  },
  table: {
    display: "table",
    width: "auto",
    borderStyle: "solid",
    borderColor: "black",
    borderWidth: 1,
    borderRightWidth: 0,
    borderBottomWidth: 0,
    marginTop: "1cm"
  },
  tableRow: {
    margin: "auto",
    flexDirection: "row"
  },
  tableColHeader: {
    width: "25%",
    borderStyle: "solid",
    borderColor: "black",
    borderBottomColor: "#000",
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopWidth: 0
  },
  tableCol: {
    width: "25%",
    borderStyle: "solid",
    borderColor: "black",
    borderWidth: 1,
    borderLeftWidth: 0,
    borderTopWidth: 0
  },
  tableCellHeader: {
    margin: "auto",
    margin: 5,
    fontSize: 12,
    fontWeight: 500
  },
  tableCell: {
    margin: "auto",
    textAlign: "center",

    margin: 5,
    fontSize: 10
  }
});

export class PDF extends Component {
  constructor(props) {
    super(props);
    this.state = {
      law: []
    };
  }

  componentDidMount() {
    api.get("api/Laws/" + this.props.Id).then(res => {
      const law = res.data;
      this.setState({
        law: res.data
      });
      console.log(JSON.stringify(law));
    });
  }

  render() {
    return (
      <Document>
        <Page size="A4" style={styles.page}>
          <View style={styles.header}>
            <Text style={styles.header}>KARTA DO GŁOSOWANIA NAD UCHWAŁAMI</Text>
            <Text style={styles.headerlow}>
              Z dnia: {this.state.law.dateAdd}
            </Text>

            <Text style={styles.textName}>
              Imie Nazwisko
              .......................................................
            </Text>
            <Text style={styles.textName}>
              Adres: .......................................................
            </Text>

            <Text style={styles.lawName}>{this.state.law.name}</Text>
            <Text style={styles.lawCount}>{this.state.law.lawText}</Text>
          </View>

          <View style={styles.table}>
            <View style={styles.tableRow}>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell}>Za</Text>
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell}>Przeciw</Text>
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell}>Wstrzymał sie</Text>
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell}>Data</Text>
              </View>
            </View>
            <View style={styles.tableRow}>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell} />
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell}> </Text>
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell} />
              </View>
              <View style={styles.tableCol}>
                <Text style={styles.tableCell} />
              </View>
            </View>
          </View>

          <View>
            <Text style={styles.signup}>Podpis Własciciela</Text>
          </View>
        </Page>
      </Document>
    );
  }
}

export default PDF;

import React, { Component } from 'react';
import ContactCard from './ContactCard';


class Background extends Component {

    constructor(props){
      super(props);
      this.state = {
        contacts: []
      }
    }

    componentDidMount() {
        fetch('https://localhost:5000/api/contacts')
          .then(response => response.json())
          .then(contacts => this.setState({ contacts }));
    }
  
    render() {
      return (
        <div>
          {this.state.contacts.map((contact, i) => <ContactCard contact={contact} key={i} />)}
        </div>
      )
    }
}

export default Background;
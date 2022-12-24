import React, { Component } from 'react';
import "./Contact.css"


class Contact extends Component {

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
      let contactDiv = this.state.contacts.map((contact) => (
        `<div class="card">
          <img src="	https://thispersondoesnotexist.com/image.jpeg" alt="Avatar" style="width:100%">
          <div class="container">
            <h4><b>${contact.name}</b></h4>
            <p>${contact.email}</p>
          </div>
        </div>`
      ))
      
      return(
        {contactDiv}
      )
    }
}

export default Contact;
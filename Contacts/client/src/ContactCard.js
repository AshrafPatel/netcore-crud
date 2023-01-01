import "./Contact.css";
import React, { Component } from 'react';

class ContactCard extends Component {
    
    constructor(props){
        super(props);
        this.state = {
          action:"get"
        }
    }

    render(props) {
        return (
        <div className="card">
            <img src={"https://thispersondoesnotexist.com/image"} alt="Avatar" style={{width:100 + "%"}}/>
            <div className="container">
              <h4><b>{props.contact}</b></h4>
              <p>{props}</p>
              <div className="buttonContain">
                <button className="buttonEdit">Edit</button>
                <button>Delete</button>
              </div>
            </div>
        </div>
        );
    }
}


export default ContactCard;
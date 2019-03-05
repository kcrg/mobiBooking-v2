import React, { Component } from 'react'
import '../css/log.scss';
import logo from '../img/mobitouch.png';

export default class SingIn extends Component {

    state = {
        email: '',
        password: ''
    }

    handleChange = (e) =>{
        this.setState({
            [e.target.id]: e.target.value
        })   
    }

    handleSubmit = (e) =>{
        e.preventDefault();
        console.log(this.state);
    }
  render() {
    return (
      <div id="log">
        <img src={logo} alt="logo"></img>

        <form onSubmit={this.handleSubmit}>
            <label htmlFor="email" id="mail">E-mail:</label>
            <input type="email" id="email" onChange={this.handleChange}></input><br/>
            <label htmlFor="password" id="pass">Hasło:</label>
            <input type="password" id="password" onChange={this.handleChange}></input><br/>

            <input type="submit" value="Zaloguj się"></input>
        </form>
      </div>
    )
  }
}

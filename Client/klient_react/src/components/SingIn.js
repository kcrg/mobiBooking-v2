import React, { Component } from 'react';
import '../css/SignIn.scss';
import logo from '../img/mobitouch.png';
import axios from 'axios';

export default class SingIn extends Component {

    state = {
        Email: null,
        Password: null
    }

    sendRequest = () =>{
      axios.post('http://192.168.43.134:51290/api/Authenticate/authenticate', this.state)
    .then(res => {
      console.log(res);
      this.setState({
        res: res.data
      })
      return res;
    }).catch(res =>{
    });
    }

    componentDidMount(){
      document.body.style.backgroundColor = "#8d1be5";
    }

    handleChange = (e) =>{
        this.setState({
            [e.target.id]: e.target.value
        })   
    }

    handleSubmit = (e) =>{
        e.preventDefault();
        this.sendRequest();
        this.props.history.push('/home');
    }
  render() {
    return (
        <div id="log">
          <img src={logo} alt="logo" id="logging"></img>

          <form onSubmit={this.handleSubmit}>
              <label htmlFor="email" id="mail">E-mail:</label>
              <input type="email" id="Email" onChange={this.handleChange}></input><br/>
              <label htmlFor="password" id="pass">Hasło:</label>
              <input type="password" id="Password" onChange={this.handleChange}></input><br/>

              <input type="submit" value="Zaloguj się"></input>
          </form>
        </div>
    )
  }
}

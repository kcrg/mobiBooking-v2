import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../css/SignIn.scss';
import logo from '../img/mobitouch.png';
import axios from 'axios';

class SingIn extends Component {

    state = {
      formData: {
        Email: null,
        Password: null
      },
        res: []
    }

    componentDidMount(){
      const { cookies } = this.props;
      if(cookies.get('token') !== undefined){
        this.props.history.push('/home');
        console.log("Token istnieje")
      }

      document.body.style.backgroundColor = "#8d1be5";
    }

    sendRequest = () =>{
      axios.post('http://192.168.10.240:51290/api/Authenticate', this.state.formData)
    .then(res => {
      const { cookies } = this.props;
      cookies.set('token', res.data.token, {path: '/'});
      if(res.status === 200)
        this.props.history.push('/home');
      return res;
    }).catch(err =>{
      console.log(err);
    });
    }


    handleChange = (name, value) =>{
        this.setState(prevState => ({
          ...prevState,
          formData: {
            ...prevState.formData,
            [name]: value
          } 
        }))
    }

    handleSubmit = (e) =>{
        e.preventDefault();
        this.sendRequest();
    }
  render() {

    return (
        <div id="log">
          <img src={logo} alt="logo" id="logging"></img>

          <form onSubmit={this.handleSubmit}>
              <label htmlFor="email" id="mail">E-mail:</label>
              <input type="email" id="Email" onChange={e => this.handleChange('Email', e.target.value)}></input><br/>
              <label htmlFor="password" id="pass">Hasło:</label>
              <input type="password" id="Password" onChange={e => this.handleChange('Password', e.target.value)}></input><br/>

              <input type="submit" value="Zaloguj się"></input>
          </form>
        </div>
    )
  }
}

export default withRouter(SingIn);

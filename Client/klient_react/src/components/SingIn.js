import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import logo from '../img/mobitouch.png';
import axios from 'axios';
import '../styles/SignIn.scss';

class SingIn extends Component {

  state = {
    formData: {
      Email: null,
      Password: null
    },
    error: 'default'
  }

  componentDidMount(){
    const { cookies } = this.props;
    if(cookies.get('token') !== undefined){
      this.props.history.push('/home');
    }
  }

  sendRequest = () =>{
    const { ip } = this.props
    axios.post(ip + '/api/Account/login', this.state.formData)
    .then(res => {
      const { cookies } = this.props;
      cookies.set('token', res.data.token, {path: '/'});
      if(res.status === 200)
        this.props.history.push('/home');
      return res;
      })
    .catch(err =>{
      this.toggleError()
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

  toggleError = () =>{
    this.setState({error: 'error'});
  }
  render() {
    return (
      <div className="content">

        <div className="logo">
          <img src={logo} alt="logo"></img>
        </div>

        <form onSubmit={this.handleSubmit} className="form">
          <label htmlFor="email" id="mail">E-mail:</label>
          <input type="email" id="Email" onChange={e => this.handleChange('Email', e.target.value)} required></input>
          <label htmlFor="password" id="pass">Hasło:</label>
          <input type="password" id="Password" onChange={e => this.handleChange('Password', e.target.value)} required></input>

          <input type="submit" value="Zaloguj się"></input>
          
          <div className={this.state.error}>
            <h1>Nie znaleziono użytkownika o danej kombinacji e-mail i hasła!</h1>
          </div>
        </form>
      </div>
    )
  }
}

export default withRouter(SingIn);

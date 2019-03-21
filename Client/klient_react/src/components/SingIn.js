import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import logo from '../img/mobitouch_w.png';
import axios from 'axios';
import '../styles/SignIn.scss';

class SingIn extends Component {

  state = {
    formData: {
      Email: '',
      Password: ''
    },
    error: 'default',
    warning: 'default_warning'
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
    }), () =>{
      console.log(this.state.formData.Email)
    })
  }

  handleSubmit = (e) =>{
    e.preventDefault();
    if(this.state.formData.Email !== '' && this.state.formData.Password !== ''){
    this.sendRequest();
    }else{
      this.setState({
        warning: 'warning'
      }, () =>{
        setTimeout(() =>{
          this.setState({warning: 'default_warning'});
         }, 3000);
      })
    }
  }

  toggleError = () =>{
    this.setState({error: 'error'});
    setTimeout(() =>{
      this.setState({error: 'default'});
     }, 3000);
  }
  
  render() {
    return (
      <div className="content">

        <div className="logo">
          <img src={logo} alt="logo"></img>
        </div>

        <form onSubmit={this.handleSubmit} className="form">
          <div className="crudentials">
            <div className="crudentialsv2">
              <label htmlFor="email" id="mail">E-mail:</label>
              <input type="email" id="Email" onChange={e => this.handleChange('Email', e.target.value)}
              placeholder="Wprowadź swój e-mail"></input>
            </div>
          </div>

          <div className="crudentials">
            <div className="crudentialsv2">
              <label htmlFor="password" id="pass">Hasło:</label>
              <input type="password" id="Password" onChange={e => this.handleChange('Password', e.target.value)}
              placeholder="Wprowadź swoje hasło"></input>
            </div>
          </div>

          <div className="submit">
            <input type="submit" value="Zaloguj się"></input>
          </div>
          
          <div className={this.state.error}>
            <p>Nie znaleziono użytkownika o danej kombinacji e-mail i hasła!</p>
          </div>

          <div className={this.state.warning}>
            Uzupełnij wwszystkie pola!
          </div>

        </form>
      </div>
    )
  }
}

export default withRouter(SingIn);

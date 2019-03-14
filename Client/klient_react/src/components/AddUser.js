import React, { Component } from 'react';
import axios from 'axios';
import '../css/AddUser.scss';
import { withRouter } from 'react-router-dom';

class AddUser extends Component {

  state = {
    userData:{
      userName: null,
      password: null,
      name: null,
      surname: null,
      email: null,
      userType: 'Administrator'
    },
    r_pass: null,
    error: 'default',
    succes: 'default'
  }

  componentDidMount(){
    const { cookies } = this.props;
    if(cookies.get('token') === undefined){
      this.props.history.push('/');
    }
  }

  sendData = () =>{
    const { ip } = this.props;
    axios.post(ip + '/api/Account/create', this.state.userData)
    .then(res => {
      this.toggleError(false)
      return res;
    })
    .catch(err =>{
      this.toggleError(true)
    });
  }

  handleSubmit = (e) =>{
    e.preventDefault();
    this.sendData();
  }

  toggleError = (error) =>{
    if( error === true ){
      this.setState({
        error: 'wrong',
        succes: 'default'
      })
    }
    else{
      this.setState({
        succes: 'done',
        error: 'default'
      })
    };
  }

   
  handleChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      userData: {
        ...prevState.userData,
        [name]: value === 'Zwykły użytkownik' ? ('User') : (value)
      } 
    }))
  }

  handleRpasswordChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      [name]: value
    }))
  }


  render() {
    return (
      <div className="content">
        <div className="form">
          <h2>Dodaj użytkownika:</h2>
          <form onSubmit={this.handleSubmit}>
            <label htmlFor="user_name">Nazwa użytkownika:</label>
            <input type="text" id="user_name" onChange={e => this.handleChange('userName', e.target.value)} required></input><br/>  

            <label htmlFor="pass">Hasło:</label>
            <input type="password" id="pass" onChange={e => this.handleChange('password', e.target.value)} required></input><br/>  

            <label htmlFor="r_pass">Powtórz hasło:</label>
            <input type="password" id="r_pass" onChange={e => this.handleRpasswordChange('r_pass', e.target.value)} required></input><br/>  

            <label htmlFor="f_name">Imię:</label>
            <input type="text" id="f_name" onChange={e => this.handleChange('name', e.target.value)} required></input><br/>  

            <label htmlFor="l_name">Nazwisko:</label>
            <input type="text" id="l_name" onChange={e => this.handleChange('surname', e.target.value)} required></input><br/>  

            <label htmlFor="email">Email:</label>
            <input type="email" id="email" onChange={e => this.handleChange('email', e.target.value)} required></input><br/> 

            <label htmlFor="permissions">Uprawnienia:</label>
            <select id="permissions" onChange={e => this.handleChange('userType', e.target.value)}>
              <option>Administrator</option>
              <option>Zwykły użytkownik</option>
            </select> 

            <input type="submit" value="Zapisz" disabled={this.state.userData.password !== this.state.r_pass}></input>
            <span className={this.state.error}>Istnieje użytkownik o podanym adresie e-mail lub nazwie użytkownika</span>
            <span className={this.state.succes}>Pomyślnie dodano użytkownika</span>
          </form>
        </div>
      </div>
    )
  }
}

export default withRouter(AddUser);
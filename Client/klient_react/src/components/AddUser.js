import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddUser.scss';

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
        error: 'wrong'
      })
      setTimeout(() =>{
        this.setState({error: 'default'});
       }, 3000);
    }
    else{
      this.setState({
        succes: 'done'
      })
      setTimeout(() =>{
        this.setState({succes: 'default'});
       }, 3000);
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
      <div className="user_form_div">
        <h2>Dodaj użytkownika:</h2>
          <form onSubmit={this.handleSubmit} className="user_form">

            <div className="user_name">
              <label htmlFor="user_name">Nazwa użytkownika: <span className="star">*</span></label>
              <input type="text" id="user_name" onChange={e => this.handleChange('userName', e.target.value)} required placeholder="Nazwa użytkownika..."></input> 
            </div>

            <div className="password">
              <label htmlFor="pass">Hasło: <span className="star">*</span></label>
              <input type="password" id="pass" onChange={e => this.handleChange('password', e.target.value)} required placeholder="Hasło..."></input>  
            </div>

            <div className="r_password">
              <label htmlFor="r_pass">Powtórz hasło: <span className="star">*</span></label>
              <input type="password" id="r_pass" onChange={e => this.handleRpasswordChange('r_pass', e.target.value)} required placeholder="Powtórz hasło..."></input>  
            </div>

            <div className="name">
              <label htmlFor="f_name">Imię:</label>
              <input type="text" id="f_name" onChange={e => this.handleChange('name', e.target.value)} required placeholder="Imię..."></input> 
            </div>

            <div className="surname">
              <label htmlFor="l_name">Nazwisko:</label>
              <input type="text" id="l_name" onChange={e => this.handleChange('surname', e.target.value)} required placeholder="Nazwisko"></input> 
            </div>

            <div className="email">
              <label htmlFor="email">Email:  <span className="star">*</span></label>
              <input type="email" id="email" onChange={e => this.handleChange('email', e.target.value)} required placeholder="E-mail"></input>
            </div>

            <div className="permissions">
              <label htmlFor="permissions">Uprawnienia:</label>
              <select id="permissions" onChange={e => this.handleChange('userType', e.target.value)}>
                <option>Administrator</option>
                <option>Zwykły użytkownik</option>
              </select>
            </div>

            <div className="add_user_submit">
              <input type="submit" value="Zapisz" disabled={this.state.userData.password !== this.state.r_pass}></input>
            </div>

          </form>

        <div className={this.state.error}>
          <p>Istnieje użytkownik o podanej nazwie użytkownika lub adresie e-mail!</p>
        </div>

        <div className={this.state.succes}>
          <p>Pomyślnie dodano użytkownika!</p>
        </div>
    </div>
    )
  }
}

export default withRouter(AddUser);
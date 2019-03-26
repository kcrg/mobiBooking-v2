import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddUser.scss';
import { connect } from 'react-redux';
import updateUser from '../actions/UpdateUser';

class EditUser extends Component {

  state = {
    r_pass: '',
    error: 'default',
    succes: 'default',
    warning: 'default',
    difPass: 'default'
  }

  
  updateUser = () =>{
    const { ip } = this.props
    const { id } = this.props.user
    axios.put(ip + '/api/Account/update/' + id, this.props.user)
    .then(res => {
        this.toggleError(false)
        if(res.status === 200){
            this.props.history.push('/userView')
        }
      return res;
    })
    .catch(err =>{
      this.toggleError(true)
    });
  }

  handleSubmit = (e) =>{
    e.preventDefault();
    if(this.checkData()){
      this.setState({
        warning: 'warning'
      }, () =>{
        setTimeout(() =>{
          this.setState({warning: 'default'});
         }, 3000);
      })
    }else{
      if(this.props.user.password !== this.state.r_pass){
        this.setState({
          difPass: 'diff_pass'
        }, () =>{
          setTimeout(() =>{
            this.setState({difPass: 'default'});
           }, 3000);
        })
      }else
      this.updateUser();
    }
  }

  checkData = () =>{
    return (this.props.user.userName.match(/^ *$/) !== null ||
    this.props.user.password.match(/^ *$/) !== null ||
    this.state.r_pass.match(/^ *$/) !== null ||
    this.props.user.email.match(/^ *$/) !== null)
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
     this.props.updateUser(name, value)
  }

  handleRepeatChange = (value) =>{
      this.setState({
          r_pass: value
      })
  }

  render() {
      console.log(this.props.user)
    return (
      <div className="user_form_div">
        <h2>Edytuj użytkownika:</h2>
          <form onSubmit={this.handleSubmit} className="user_form">

            <div className="user_name">
              <div className="user_name_label">
                <label htmlFor="user_name">Nazwa użytkownika:  <span className="star">*</span></label>
              </div>
              <div className="user_name_input">
                <input type="text" id="user_name" value={this.props.user.userName} onChange = {e =>{this.handleChange('userName', e.target.value)}} placeholder="Nazwa użytkownika..."></input> 
              </div>
            </div>

            <div className="password">
              <div className="password_label">
                <label htmlFor="pass">Hasło: <span className="star">*</span></label>
              </div>
              <div className="password_input">
                <input type="password" id="pass"  onChange = {e =>{this.handleChange('password', e.target.value)}} placeholder="Hasło..."></input>  
              </div>
            </div>

            <div className="r_password">
              <div className="r_password_label">
                <label htmlFor="r_pass">Powtórz hasło: <span className="star">*</span></label>
              </div>
              <div className="r_password_input">
                <input type="password" id="r_pass" onChange = {e =>{this.handleRepeatChange(e.target.value)}} placeholder="Powtórz hasło..."></input> 
              </div> 
            </div>

            <div className="name">
              <div className="name_label">
                <label htmlFor="f_name">Imię:</label>
              </div>
              <div className="name_input">
                <input type="text" id="f_name" value={this.props.user.name} onChange = {e =>{this.handleChange('name', e.target.value)}} placeholder="Imię..."></input> 
              </div>
            </div>

            <div className="surname">
              <div className="surname_label">
                <label htmlFor="l_name">Nazwisko:</label>
              </div>
              <div className="surname_input">
                <input type="text" id="l_name" value={this.props.user.surname} onChange = {e =>{this.handleChange('surname', e.target.value)}} placeholder="Nazwisko"></input> 
              </div>
            </div>

            <div className="email">
              <div className="email_label">
                <label htmlFor="email">Email:  <span className="star">*</span></label>
              </div>
              <div className="email_input">
                <input type="email" id="email" value={this.props.user.email} onChange = {e =>{this.handleChange('email', e.target.value)}} placeholder="E-mail"></input>
              </div>
            </div>

            <div className="permissions">
              <div className="permissions_label">
                <label htmlFor="permissions">Uprawnienia:</label>
              </div>
              <div className="permissions_select">
                <select id="permissions" selected={this.props.user.role} onChange = {e =>{this.handleChange('userType', e.target.value)}}>
                  <option>Administrator</option>
                  <option>Zwykły użytkownik</option>
                </select>
              </div>
            </div>

            <div className="add_user_submit">
              <input type="submit" value="Zapisz"></input>
            </div>

            <div className={this.state.error}>
              <p>Wprowadzono niepoprawne dane!</p>
            </div>

            <div className={this.state.succes}>
              <p>Zaktualizowano dane użytkownika!</p>
            </div>

            <div className={this.state.warning}>
              <p>Uzupełnij wymagane pola!</p>
            </div>

            <div className={this.state.difPass}>
              <p>Hasła nie są zgodne!</p>
            </div>
          </form>
    </div>
    )
  }
}

const mapStateToProps = (state) =>{
  return{
      user: state.user
  }
}

const mapDispatchToProps = (dispatch) =>{
    return {
        updateUser: (name, value) => {dispatch(updateUser(name,value))}
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(EditUser));
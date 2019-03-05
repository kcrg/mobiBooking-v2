import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import SignIn from './components/SingIn';
class App extends Component {

  state = {
    email: '',
    password: ''
  }

  addPass = (pasy) =>{
    let email = pasy.email;
    let password = pasy.password
   this.setState({
     email: email,
     password: password
   })
  }

  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <SignIn addPass={this.addPass} />
        </div>
      </BrowserRouter>
    );
  }
}

export default App;

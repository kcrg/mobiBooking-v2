import React, { Component } from 'react';
import { BrowserRouter, Route} from 'react-router-dom';
import SignIn from './components/SingIn';
import NavBar from './components/NavBar';
import Logout from './components/Logout';
import Dashboard from './components/Dashboard';
import axios from 'axios';

class App extends Component {

  state = {
    email: '',
    password: ''
  }

  sendRequest = () =>{
    axios.post('http://192.168.10.240:51290/api/Users/authenticate', this.state)
  .then(res => {
    return res;
  });
  }

  addPass = (pasy) =>{
    let email = pasy.email;
    let password = pasy.password
    this.setState({
      email: email,
      password: password
    }, (this.sendRequest))
  }

  
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Route exact path="/" render={(props) =><SignIn {...props} addPass={this.addPass}/>}/>
          <Route path="/home" component={NavBar}/>
          <Route path="/home" component={Logout}/>
          <Route path="/home" component={Dashboard}/>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;

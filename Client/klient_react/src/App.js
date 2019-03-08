import React, { Component } from 'react';
import { BrowserRouter, Route} from 'react-router-dom';
import { withCookies } from 'react-cookie';
import Dashboard from './components/Dashboard';
import SignIn from './components/SingIn';
import NavBar from './components/NavBar';
import Logout from './components/Logout';
import AddUser from './components/AddUser';
class App extends Component {


  
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Route exact path="/" render={() => <SignIn cookies={this.props.cookies}/>}/>
          <Route path="/home" component={NavBar}/>
          <Route path="/home" component={Logout}/>
          <Route  path="/home" component={Dashboard}/>
         
          <Route path="/addUser" component={NavBar}/>
          <Route path="/addUser" component={Logout}/>
          <Route path="/addUser" component={AddUser}/>
        </div>
      </BrowserRouter>
    );
  }
}

export default withCookies(App);

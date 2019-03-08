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
          <Route path="/home" render={() => <Logout cookies={this.props.cookies}/>}/>
          <Route  path="/home" render={() => <Dashboard cookies={this.props.cookies}/>}/>
         
          <Route path="/addUser" component={NavBar}/>
          <Route path="/addUser" render={() => <Logout cookies={this.props.cookies}/>}/>
          <Route path="/addUser" render={() => <AddUser cookies={this.props.cookies}/>}/>

        </div>
      </BrowserRouter>
    );
  }
}

export default withCookies(App);

import React, { Component } from 'react';
import { BrowserRouter, Route} from 'react-router-dom';
import { withCookies } from 'react-cookie';
import Dashboard from './components/Dashboard';
import SignIn from './components/SingIn';
import NavBar from './components/NavBar';
import Logout from './components/Logout';
import AddUser from './components/AddUser';
import AddRoom from './components/addRoom';
import RoomReserv from './components/roomReserv';
import UserView from './components/UserView';
import EditUser from './components/EditUser';
import RoomView from './components/RoomView';
import EditRoom from './components/EditRoom'


class App extends Component {

  render() {
    const ipServer = 'https://mobibookingwebapi.azurewebsites.net';
    return (
      <BrowserRouter>
        <div className="App">
          <Route exact path="/" render={() => <SignIn cookies={this.props.cookies} ip={ipServer}/>}/>

          <Route path="/home" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/home" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route  path="/home" render={() => <Dashboard cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>
         
          <Route path="/addUser" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/addUser" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/addUser" render={() => <AddUser cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

         
          <Route path="/addRoom" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/addRoom" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/addRoom" render={() => <AddRoom cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

          <Route path="/roomReserv" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/roomReserv" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/roomReserv" render={() => <RoomReserv cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

          <Route path="/userView" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/userView" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/userView" render={() => <UserView cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

          
          <Route path="/editUser" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/editUser" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/editUser" render={() => <EditUser cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

          <Route path="/roomView" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/roomView" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/roomView" render={() => <RoomView cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>

          
          <Route path="/editRoom" render={() => <Logout cookies={this.props.cookies} ip={ipServer}/>}/>
          <div className="full">
            <Route path="/editRoom" render={() => <NavBar cookies={this.props.cookies}/>}/>
            <Route path="/editRoom" render={() => <EditRoom cookies={this.props.cookies} ip={ipServer}/>}/>
          </div>
        </div>
      </BrowserRouter>
    );
  }
}

export default withCookies(App);

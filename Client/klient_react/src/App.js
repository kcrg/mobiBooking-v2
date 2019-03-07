import React, { Component } from 'react';
import { BrowserRouter, Route} from 'react-router-dom';
import SignIn from './components/SingIn';
import NavBar from './components/NavBar';
import Logout from './components/Logout';
import Dashboard from './components/Dashboard';
class App extends Component {

  state = {
    res: null
  }
  
  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <Route exact path="/" component={SignIn}/>
          <Route path="/home" component={NavBar}/>
          <Route path="/home" component={Logout}/>
          <Route path="/home" component={Dashboard}/>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;

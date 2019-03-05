import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import SignIn from './components/SingIn';
class App extends Component {

  state = {
    email: '',
    password: ''
  }

  render() {
    return (
      <BrowserRouter>
        <div className="App">
          <SignIn />
        </div>
      </BrowserRouter>
    );
  }
}

export default App;

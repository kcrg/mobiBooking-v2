import React, { Component } from 'react'

export default class UserTemp extends Component {

  state = {
    class: 'origin',
    isChecked: false
  }

  buttonClick = (e) =>{
    const {user} = this.props;
    if(!this.state.isChecked){
      this.setState({
        class: 'focused',
        isChecked: true
      })
      this.props.AddUser(user);
    }
    else{
      this.setState({
        class: 'origin',
        isChecked: false
      })
      this.props.DeleteUser(user);
    }
  }

  render() {
    const { user } = this.props
    return (
      <div id={user.id} className={this.state.class} onClick={e =>{this.buttonClick(e)}}>
        {user.userName}
      </div>
    )
  }
}

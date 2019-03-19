import React, { Component } from 'react'

export default class UserTemp extends Component {

  state = {
    class: 'origin',
    isChecked: false
  }

  buttonClick = (e) =>{
    const {id} = this.props;
    if(!this.state.isChecked){
      this.setState({
        class: 'focused',
        isChecked: true
      })
      this.props.AddUser(id);
    }
    else{
      this.setState({
        class: 'origin',
        isChecked: false
      })
      this.props.DeleteUser(id);
    }
  }

  render() {
    const { id, name, surname} = this.props
    return (
      <div id={id} className={this.state.class} onClick={e =>{this.buttonClick(e)}}>
        {name + " " + surname}
      </div>
    )
  }
}

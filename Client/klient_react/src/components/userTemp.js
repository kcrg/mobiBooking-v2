import React, { Component } from 'react'

export default class UserTemp extends Component {

  state = {
    class: 'origin',
    isChecked: false
  }

  buttonClick = (e) =>{
    if(e.target.className === 'origin'){
      this.setState({
        class: 'focused',
        isChecked: true
      }, () =>{
        console.log(this.state)
      })
    }
    else{
      this.setState({
        class: 'origin',
        isChecked: false
      }, () =>{
        console.log(this.state)
      })
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

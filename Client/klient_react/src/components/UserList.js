import React, { Component } from 'react';
import Users from './Users';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowCircleRight, faArrowCircleLeft } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

library.add(faArrowCircleRight, faArrowCircleLeft)

export default class UserList extends Component {
    state = {
        usersLeft: [],
        usersRight: [],
        selectedUsersLeft: [],
        selectedUsersRight: [],
        clearSelected: false
    }

    componentDidMount(){
        this.getUsers()
    }

    getUsers = () =>{
        const { ip } = this.props
        axios.get( ip + '/api/Users/get_all')
        .then(res =>{
            this.setState({
               usersLeft: res.data 
            })
        })
      }

      arrowRightClick = () =>{
        if(this.state.usersLeft.length === 0 && this.state.selectedUsersLeft.length === 0){
          return;
        }
        this.setState({
            usersRight: this.state.usersRight.concat(this.state.selectedUsersLeft)
        })
        const filteredUsers = this.state.usersLeft.filter(user =>!this.state.selectedUsersLeft.includes(user));
        this.setState({
            usersLeft: filteredUsers,
            clearSelected: true,
            selectedUsersLeft: []
      })
      }

    
      arrowLeftClick = () =>{
        if(this.state.usersRight.length === 0 && this.state.selectedUsersRight.length === 0){
          return;
        }
        this.setState({
          usersLeft: this.state.usersLeft.concat(this.state.selectedUsersRight)
        })
        const filteredUsers = this.state.usersRight.filter(user =>!this.state.selectedUsersRight.includes(user));
        this.setState({
           usersRight: filteredUsers,
           selectedUsersRight: [],
           clearSelected: true
      })
      }
    
      setSelectedUsersLeft = (selectedUsers) =>{
        this.setState({
          selectedUsersLeft: selectedUsers,
        })
      }
    
      setSelectedUsersRight = (selectedUsers) =>{
        this.setState({
          selectedUsersRight: selectedUsers,
        })
      }

      disableClearSelected = () =>{
          this.setState({
              clearSelected: false
          })
      }

  render() {
    return (
      <div className="users_div">
        <Users users={this.state.usersLeft} setSelectedUsers={this.setSelectedUsersLeft}/>
        <div className="arrows">
            <FontAwesomeIcon icon={faArrowCircleLeft} onClick = {this.arrowLeftClick}></FontAwesomeIcon>
            <FontAwesomeIcon icon={faArrowCircleRight} onClick = {this.arrowRightClick}></FontAwesomeIcon>
        </div>
        <Users users={this.state.usersRight} setSelectedUsers={this.setSelectedUsersRight}/>
    </div>
    )
  }
}

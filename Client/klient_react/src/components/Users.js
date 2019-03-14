import React, { Component } from 'react';
import UserTemp from './userTemp';
import axios from 'axios';

export default class Users extends Component {

    state = {
        users: null,
        userItem: null,
    }

    componentDidMount(){
        const { ip } = this.props
        axios.get( ip + '/api/Users/get_all')
        .then(res =>{
            this.setState({
               users: res.data 
            }, this.mapItems)
        })
    }

    mapItems = () =>{
    const userItem = this.state.users.map(user =>{
    return(
        <div key={user.id} className="origin">
            <UserTemp id={user.id} name={user.name} surname={user.surname}></UserTemp>
        </div>
    )
    })
    this.setState({
      userItem
    })
  }
    render() {
        return (
         <div className="users">
            {this.state.userItem}
        </div>
        )
    }
}

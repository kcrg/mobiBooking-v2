import React, { Component } from 'react';
import UserTemp from './userTemp';


export default class Users extends Component {

    state = {
        users: null,
        userItem: null,
        selectedUsers: []
    }

    componentWillReceiveProps(){
        const { users } = this.props
        if(users != null){
            this.setState({
                users
            }, this.mapItems)
        }
    }

    AddSelectedUser = (id) => {
        console.log('AddSelectedUser ' + id);
        this.setState(prevState => ({
            selectedUsers: [...prevState.selectedUsers, id]
          }), () => console.log(this.state.selectedUsers))
    }

    DeleteSelectedUser = (id) => {
        const filteredUsers  = this.state.selectedUsers.filter(idt => idt !== id)
        this.setState({
            selectedUsers: filteredUsers
        }, () => console.log(this.state.selectedUsers))
    }

    mapItems = () =>{
    const userItem = this.state.users.map(user =>{
    return(
        <div key={user.id}>
            <UserTemp id={user.id} name={user.name} surname={user.surname} AddUser = {this.AddSelectedUser} DeleteUser = {this.DeleteSelectedUser}></UserTemp>
        </div>
    )
    })
    this.setState({
      userItem
    }, this.getSelectedUsers)
  }
    render() {
        return (
         <div className="users">
            {this.state.userItem}
        </div>
        )
    }
}

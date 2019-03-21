import React, { Component } from 'react';
import UserTemp from './userTemp';


export default class Users extends Component {

    state = {
        users: null,
        userItem: null,
        selectedUsers: []
    }

    componentWillReceiveProps(nextProps){
        const users = this.props.users
        const newUsers = nextProps.users
        if(newUsers !== null){
            this.setState({
                users : newUsers
            }, this.mapItems)
        }

        if(newUsers !== users) {     
            this.setState({
                selectedUsers: []
            })
        }
    }

    AddSelectedUser = (user) => {
        // const { disableClearSelected } = this.props
        // disableClearSelected();
        const { setSelectedUsers } = this.props
        this.setState(prevState => ({
            selectedUsers: [...prevState.selectedUsers, user]
          }), () => setSelectedUsers(this.state.selectedUsers))
    }

    DeleteSelectedUser = (user) => {
        // const { disableClearSelected } = this.props
        // disableClearSelected();
        const filteredUsers  = this.state.selectedUsers.filter(usert => usert !== user)
        this.setState({
            selectedUsers: filteredUsers
        }, () => console.log(this.state.selectedUsers))
    }

    mapItems = () =>{
    const userItem = this.state.users.map(user =>{
    return(
        <div key={user.id}>
            <UserTemp user={user} AddUser = {this.AddSelectedUser} DeleteUser = {this.DeleteSelectedUser}></UserTemp>
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

import React, { Component } from 'react';
import UserTemp from './userTemp';


export default class Users extends Component {

    state = {
        users: null,
        userItem: null,
        selectedUsers: [],
        title: ''
    }

    componentDidMount(){
        const { isLeft } = this.props
            this.setState({
                title: isLeft ? "Wybierz uczestników:" : "Uczestnicy:"
            })
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
        const { setSelectedUsers } = this.props
        this.setState(prevState => ({
            selectedUsers: [...prevState.selectedUsers, user]
          }), () =>{
              setSelectedUsers(this.state.selectedUsers)
          })
    }

    DeleteSelectedUser = (user) => {
        const { setSelectedUsers } = this.props
        const filteredUsers  = this.state.selectedUsers.filter(usert => usert !== user)
        this.setState({
            selectedUsers: filteredUsers
        }, () =>{
            setSelectedUsers(this.state.selectedUsers)})
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
            <div className="users_mess">
                <span>{this.state.title} <span className="star">*</span></span>
                <div className="users">
                    {this.state.userItem}
                </div>
            </div>
        )
    }
}

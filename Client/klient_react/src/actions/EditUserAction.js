import axios from 'axios';

const editUser = (id, ip) =>{
    return(dispatch) =>{
        axios.get( ip + '/api/Users/get/' + id)
        .then(res =>{
            dispatch({type: 'GET_USER_DATA', user:
            {userName : res.data.userName,
                id: id,
                name: res.data.name,
                password: '',
                surname: res.data.surname,
                email:res.data.email,
                userType: ''
            }

        })
        })
        .catch(err =>{
            console.log(err)
        })
    } 
}

export default editUser
import axios from 'axios';

const getUsers = (ip) =>{
    return(dispatch) => {
        //async call
        axios.get( ip + '/api/Users/get_all')
        .then(res =>{
            dispatch({type: 'GET_USERS', data: res.data})
        })
        .catch(err =>{
            console.log(err)
        })
    }
}

export  default getUsers
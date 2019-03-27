import axios from 'axios';

const getAva = (ip) =>{
    return(dispatch) =>{
        axios.get( ip + '/api/Room/get_room_availabilities/')
        .then(res =>{
            dispatch({type: 'GET_AVA_DATA', ava: res.data})
        })
        .catch(err =>{
            console.log(err)
        })
    } 
}

export default getAva
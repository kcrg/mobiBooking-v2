import axios from 'axios';

const editRoom = (id, ip) =>{
    return(dispatch) =>{
        axios.get( ip + '/api/Room/get/' + id)
        .then(res =>{
            dispatch({type: 'GET_ROOM_DATA', room: res.data})
        })
        .catch(err =>{
            console.log(err)
        })
    } 
}

export default editRoom
import axios from 'axios'

const getRoomsNumber = (ip) =>{
    return(dispatch) => {
        axios.get(ip + '/api/Room/get_all?orderByName=false')
        .then(res =>{
            dispatch({type: 'GET_ROOMS', rooms: res.data})
        })
        .catch(err =>{

        })
    }
 }

 export default getRoomsNumber;
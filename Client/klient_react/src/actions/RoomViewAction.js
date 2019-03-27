import axios from "axios";

 const getRooms = (ip) =>{
    return(dispatch) => {
        axios.get(ip + '/api/Room/get_all?orderByName=true')
        .then(res =>{
            dispatch({type: 'GET_ROOMS', rooms: res.data})
        })
        .catch(err =>{

        })
    }
 }

export default getRooms
 

const updateRoom = (name, value) =>{
    return(dispatch) => {
       dispatch({type: 'UPDATE_ROOM', name, value})
    }
 }
 
 export default updateRoom
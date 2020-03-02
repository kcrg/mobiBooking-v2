
const updateUser = (name, value) =>{
   return(dispatch) => {
      dispatch({type: 'UPDATE_USER', name, value})
   }
}

export default updateUser
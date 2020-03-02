const reservRoom = (id,data) =>{
    return(dispatch) => {
      dispatch({type: 'SAVE_ID', object:{
          id,
          data
      }})
    }
 }

export default reservRoom
 
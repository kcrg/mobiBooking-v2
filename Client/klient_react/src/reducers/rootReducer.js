
const initState = {
    usersData : [],
    user: {
        id: null,
        userName: '',
        name: '',
        password: '',
        surname: '',
        email: '',
        userType: 'Administrator'
    }
}

const rootReducer = (state = initState, action) =>{
    if(action.type === 'GET_USERS'){
     return{
         ...state,
         usersData: action.data
     }
    }
    else if(action.type === 'GET_USER_DATA'){
        return{
            ...state,
            user: {
                ...state.user,
                id: action.user.id,
                userName: action.user.userName,
                name: action.user.name,
                password: '',
                surname: action.user.surname,
                email: action.user.email,
                userType: 'Administrator'
            }
        }
    }
    else if(action.type === 'UPDATE_USER'){
        return{
            ...state,
            user:{
                ...state.user,
                [action.name]: action.name === 'userType' ? (action.value === 'Zwykły użytkownik' ? ('User') : ('Administrator')): action.value 
            }
        }
    }
    return state
}

export default rootReducer;
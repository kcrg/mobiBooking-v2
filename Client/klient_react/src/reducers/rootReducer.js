
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
    },
    roomData: [],
    roomId: null,
    room: {
        roomName: '',
        location: '',
        availabilityId: null,
        numberOfPeople: ''
    },
    ava: [],
    id: null,
    data: null
}

const rootReducer = (state = initState, action) =>{
    if(action.type === 'GET_USERS'){
     return{
         ...state,
         usersData: action.data
     }
    }
    else if(action.type === 'GET_ROOMS'){
        return{
            ...state,
            roomData: action.rooms
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
    else if(action.type === 'UPDATE_ROOM'){
        return{
            ...state,
            room:{
                ...state.room,
                [action.name]: action.name === 'numberOfPeople' ? (action.value) : action.value
            }
        }
    }
    else if(action.type === 'GET_ROOM_DATA'){
        return{
            ...state,
            roomId: action.room.id,
            room:{
                ...state.room,
                roomName: action.room.roomName,
                location: action.room.location,
                availabilityId: action.room.availabilityId,
                numberOfPeople: action.room.numberOfPeople
            } 
        }
    }
    else if(action.type === 'GET_AVA_DATA'){
        return{
            ...state,
            ava: action.ava
        }
    }

    else if(action.type === 'SAVE_ID'){
        return{
            ...state,
            id: action.object.id,
            data: action.object.data
        }
    }
    return state
}

export default rootReducer;
// import axios from "axios";
// import {fetchCurrentUser} from './usersApi'

// const BASE_URL = "http://localhost:3000/api"

// export const registerUser = async (regData) =>{
//     const response = await axios.post(`${BASE_URL}/auth/registration`, regData);
//     console.log(response.data)

//     const loginResponse = await loginUser({
//         email: regData.email, 
//         password: regData.password 
//     });

//     return loginResponse; 
// }


// export const loginUser = async (loginData) =>{

//     const response = await axios.post(`${BASE_URL}/auth/login`, loginData);

//     console.log(response.data)
//     localStorage.setItem('accessToken', response.data.accessToken);
//     localStorage.setItem('refreshToken', response.data.refreshToken);    
//     localStorage.setItem('userId', response.data.userId); 
        
//     return response.data
// }


import axios from "axios";

const BASE_URL = "/api";

export const registerUser = async (regData) => {
    try {
        const response = await axios.post(`${BASE_URL}/auth/registration`, regData);
        console.log(response.data);

        const loginResponse = await loginUser({
            email: regData.email,
            password: regData.password
        });

        return loginResponse;
    } catch (error) {
        console.error("Ошибка регистрации:", error);
        throw error; 
    }
};

export const loginUser = async (loginData) => {
    try {
        const response = await axios.post(`${BASE_URL}/auth/login`, loginData);
        console.log(response.data);

        localStorage.setItem('accessToken', response.data.accessToken);
        localStorage.setItem('refreshToken', response.data.refreshToken);
        localStorage.setItem('userId', response.data.userId);

        return response.data;
    } catch (error) {
        console.error("Ошибка входа:", error);
        throw error; 
    }
};

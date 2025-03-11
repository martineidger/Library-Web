// import axios from "axios";


// const apiClient = axios.create({
//     baseURL: 'http://localhost:3000/api'
// });

// apiClient.interceptors.request.use(
//     config => {
//         const accessToken = localStorage.getItem('accessToken');
//         if(accessToken){
//             config.headers['Authorization'] = `Bearer ${accessToken}`;
//         }
            
//         return config;
//     },
//     error => {
//         return Promise.reject(error);
//     });


//     apiClient.interceptors.response.use(
//         response => response,
//         async error => {
//             const originalRequest = error.config;
            
//             if (error.response && error.response.status === 401) {
//                 console.log(originalRequest._retry)
//                 if (!originalRequest._retry) {
                    
//                     originalRequest._retry = true; // Устанавливаем _retry в true
//                     const newAccessToken = await refreshAccessToken(); // Запрос на обновление токена
    
//                     if (newAccessToken) {
//                         originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
//                         return apiClient(originalRequest); // Повторный запрос
//                     }
//                 }
//             } if(!error.response){ //проверка корс
                
//                 if (!originalRequest._retry) {
                    
//                     originalRequest._retry = true; 
//                     const newAccessToken = await refreshAccessToken(); 
    
//                     if (newAccessToken) {
//                         originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
//                         return apiClient(originalRequest); 
//                     }
//                 }
//             }
    
//             return Promise.reject(error);
//         }
//     );

// async function refreshAccessToken() {
//     const refreshToken = localStorage.getItem('refreshToken');

//     const response = await axios.post(`http://localhost:3000/api/auth/refresh`, refreshToken);

//     if(response.statusCode === 200){
//         return response.data.accessToken;
//     }
//     return null;
// }

// export default apiClient;

// import axios from "axios";

// const apiClient = axios.create({
//     baseURL: 'http://localhost:3000/api'
// });

// apiClient.interceptors.request.use(
//     config => {
//         const accessToken = localStorage.getItem('accessToken');
//         if (accessToken) {
//             config.headers['Authorization'] = `Bearer ${accessToken}`;
//         }
//         return config;
//     },
//     error => {
//         return Promise.reject(error);
//     }
// );

// apiClient.interceptors.response.use(
//     response => response,
//     async error => {
//         const originalRequest = error.config;
        
//         if (error.response && error.response.status === 401) {
//             console.log("ERROR ")

//             if (!originalRequest._retry) {
//                 console.log("TRY ")

//                 originalRequest._retry = true; // Устанавливаем _retry в true
//                 const newAccessToken = await refreshAccessToken(); // Запрос на обновление токена

//                 if (newAccessToken) {
//                     console.log("NEW ")

//                     originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
//                     return apiClient(originalRequest); // Повторный запрос
//                 }
//             } else {
//                 console.log("REDIRECT ")

//                 // Если обновление токена не удалось, перенаправляем на страницу логина
//                 localStorage.removeItem('accessToken');
//                 localStorage.removeItem('refreshToken');
//                 window.location.href = '/login'; // Перенаправление на страницу логина
//             }
//         } else if (!error.response) { // Проверка CORS
//             console.log("CORS ")

//             if (!originalRequest._retry) {
//                 console.log("CORS RETRY ")

//                 originalRequest._retry = true; 
//                 const newAccessToken = await refreshAccessToken(); 
//                 console.log(newAccessToken)
//                 if (newAccessToken) {
//                     console.log("CORS NEW ")

//                     originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
//                     return apiClient(originalRequest); 
//                 } else{
//                     console.log("CORS REDIRECT ")
//                     localStorage.removeItem('accessToken');
//                 localStorage.removeItem('refreshToken');
//                 window.location.href = '/login';
//                 }
//             }
//         }

//         return Promise.reject(error);
//     }
// );

// async function refreshAccessToken() {
//     const refreshToken = localStorage.getItem('refreshToken');

//     const response = await axios.post(`http://localhost:3000/api/auth/refresh`, { refreshToken });

//     if (response.status === 200) {
//         return response.data.accessToken;
//     }
//     return null;
// }

// export default apiClient;

import axios from "axios";

const apiClient = axios.create({
    baseURL: '/api'
});

apiClient.interceptors.request.use(
    config => {
        const accessToken = localStorage.getItem('accessToken');
        if (accessToken) {
            config.headers['Authorization'] = `Bearer ${accessToken}`;
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

apiClient.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;

        if (error.response) {
            if (error.response.status === 401) {
                if (!originalRequest._retry) {
                    originalRequest._retry = true;
                    const newAccessToken = await refreshAccessToken();

                    if (newAccessToken) {
                        originalRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                        return apiClient(originalRequest);
                    }
                }
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                window.location.href = '/login';
            }
        } else if (!error.response) { // cors && network
            window.location.href = '/error'; 
        }

        return Promise.reject(error);
    }
);

async function refreshAccessToken() {
    const refreshToken = localStorage.getItem('refreshToken');

    const response = await axios.post(`/api/auth/refresh`, { refreshToken });

    if (response.status === 200) {
        return response.data.accessToken;
    }
    return null;
}

export default apiClient;
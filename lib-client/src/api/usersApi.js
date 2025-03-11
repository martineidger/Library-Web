import { setUserData } from '../redux/actions';
import apiClient from './apiClient';
import { updateBookPickDate } from '../redux/actions';

export const fetchCurrentUser = async () => {
    try{    
        let currentUserId = localStorage.getItem('userId')

        const response = await apiClient.get(`/users/${currentUserId}`);
        console.log("FROM FETCH   ");
        console.log(response.data)

        localStorage.setItem('email', response.data.email); 
        localStorage.setItem('userName', response.data.displayName);
        localStorage.setItem('userRole', response.data.role); 

        return response.data
    }
    catch(error){
        console.log(error)
    }
}

export const fetchUserBooks = async (currentPage, pageSize) => {
    try{
        const response = await apiClient.get(`/users/${localStorage.getItem('userId')}/mybooks?page=${currentPage}&size=${pageSize}`);
        console.log(response.data)
        return response.data
    }
    catch(error){
        console.log(error.message)
    }

};

export const takeBook = (book) => {
    return async (dispatch) => {
        try {
            
            const response = await apiClient.post(`/users/takebook/${book.id}?userId=${localStorage.getItem('userId')}`)
            console.log(response)
            if (!response.status===200) {
                throw new Error('Ошибка при взятии книги');
            }

            dispatch(updateBookPickDate(book.Id, response.data.returnDate));
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };
};
    
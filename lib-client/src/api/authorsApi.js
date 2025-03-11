import apiClient from './apiClient';

export const fetchAuthors = async () => {
    try{
        const response = await apiClient.get(`/authors/full`);
        console.log(response.data)
        return response.data
    }
    catch(error){
        console.log(error.message)
    }

};

export const fetchAuthorsPagin = async (currentPage, pageSize) => {
    try{
        const response = await apiClient.get(`/authors?page=${currentPage}&size=${pageSize}`);
        console.log(response.data)
        return response.data
    }
    catch(error){
        console.log(error.message)
    }

};

export const addAuthor = async (author) =>{
    try{
        const response = await apiClient.post(`/authors`, author);
        console.log(response.data)
        return response.data
    }
    catch(error){
        console.log(error.message)
    }
}
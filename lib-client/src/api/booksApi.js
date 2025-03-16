import apiClient from './apiClient';

export const fetchBooks = async (currentPage, pageSize) => {
    try{
        const response = await apiClient.get(`/books?page=${currentPage}&size=${pageSize}`);
        console.log(response.data)
        return response.data
    }
    catch(error){
        console.log(error.message)
    }

};


export const updateBook = async (bookId, formData) =>{
    try{
        console.log("UPD FORMDATA  ",formData)
        const response = await apiClient.put(`/books/${bookId}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data
    }
    catch(error){
        console.log(error.message)
        throw error;
    }
}

export const deleteBook = async (bookId) =>{
    try{
        const response = await apiClient.delete(`/books/${bookId}`);
        return response
    }
    catch(error){
        console.log(error.message)
        throw error;
    }
}
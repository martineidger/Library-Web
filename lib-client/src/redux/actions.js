import { loginUser, registerUser } from '../api/authApi'; // Импортируйте функции
import { fetchCurrentUser, takeBook, fetchUserBooks } from '../api/usersApi';
import { fetchBooks, updateBook, deleteBook } from '../api/booksApi';
import { fetchAuthors, addAuthor } from '../api/authorsApi';
import apiClient from '../api/apiClient';

export const loginRequest = (credentials) => async (dispatch) => {
  dispatch({ type: 'LOGIN_REQUEST' });

  try {
    const response = await loginUser(credentials); 
    dispatch({type: 'LOGIN_SUCCESS',payload: response.data})

    const userData = await fetchCurrentUser();
    console.log(userData)
    dispatch({ type: 'SET_USER_DATA', payload: userData });
  } catch (error) {
    dispatch({ type: 'LOGIN_FAILURE', payload: error });
  }
};

export const registerRequest = (regData) => async (dispatch) => {
  dispatch({ type: 'REGISTER_REQUEST' });

  try {
    const response = await registerUser(regData); 
    dispatch({ type: 'REGISTER_SUCCESS', payload: response.data });

    const userData = await fetchCurrentUser();
    console.log(userData)
    dispatch({ type: 'SET_USER_DATA', payload: userData });
  } catch (error) {
    dispatch({ type: 'REGISTER_FAILURE', payload: error.response.data.message });
  }
};

export const getBooks = (currentPage, pageSize) => async (dispatch) => {
  dispatch({ type: 'FETCH_BOOKS_REQUEST' });

  try {
      const response = await fetchBooks(currentPage, pageSize); // Передайте параметры в fetchBooks
      dispatch({ type: 'FETCH_BOOKS_SUCCESS', payload: response });
  } catch (error) {
      dispatch({ type: 'FETCH_BOOKS_FAILURE', payload: error.response.data.message });
  }
};

export const getUserBooks = (currentPage, pageSize) => async (dispatch) => {
  dispatch({ type: 'FETCH_USBOOKS_REQUEST' });

  try {
      const response = await fetchUserBooks(currentPage, pageSize); // Передайте параметры в fetchBooks
      dispatch({ type: 'FETCH_USBOOKS_SUCCESS', payload: response });
  } catch (error) {
      dispatch({ type: 'FETCH_USBOOKS_FAILURE', payload: error });
  }
};

export const borrowBook = (book) => async (dispatch) => {
  try{
    dispatch({type: 'BORROW_BOOK',payload: book,})
    console.log('BORROW ', book)
    await dispatch(takeBook(book));
  }catch(error){
    dispatch({ type: 'BORROW_BOOK_FAILURE', payload: error.response.data.message });
  }
};

export const addBookRequest = (bookData) => async (dispatch) => {
  dispatch({ type: 'ADD_BOOK_REQUEST' });

  console.log(bookData); 

  const formData = new FormData();
  for (const key in bookData) {
      if (bookData[key] !== null && bookData[key] !== undefined) { 
          formData.append(key, bookData[key]);
      }
  }

  for (const pair of formData.entries()) {
      console.log(`${pair[0]}: ${pair[1]}`); // Отладка
  }

  try {
      const response = await apiClient.post('/books', formData, {
          headers: {
              'Content-Type': 'multipart/form-data',
          },
      });
      dispatch({ type: 'ADD_BOOK_SUCCESS', payload: response.data });
  } catch (error) {
      dispatch({ type: 'ADD_BOOK_FAILURE', payload: error.message });
  }
};

export const updateBookRequest = (bookData, bookId) => async (dispatch) => {
  dispatch({ type: 'UPDATE_BOOK_REQUEST' });

  console.log("BOOKDATA  ", bookData)

  const formData = new FormData();
  for (const key in bookData) {
      if (bookData[key] !== null && bookData[key] !== undefined) { 
          formData.append(key, bookData[key]);
      }
  }

  console.log("formdata log: ")
  for (const pair of formData.entries()) {
      console.log(`${pair[0]}: ${pair[1]}`); 
  }

  console.log("FORMDATA   ", formData)

  try {
      const response = await updateBook(bookId, formData)
      dispatch({ type: 'UPDATE_BOOK_SUCCESS', payload: response });
  } catch (error) {
      dispatch({ type: 'UPDATE_BOOK_FAILURE', payload: error.message });
  }
};

export const deleteBookRequest = (bookId) => async (dispatch) =>{
  dispatch({ type: 'DELETE_BOOK_REQUEST' });

  try {
    const response = await deleteBook(bookId); 
    dispatch({type: 'DELETE_BOOK_SUCCESS', payload: bookId})

  } catch (error) {
    dispatch({ type: 'DELETE_BOOK_FAILURE', payload: error });
  }
}


export const updateBookPickDate = (bookId, returnDate) => ({
  type: 'UPDATE_BOOK_PICK_DATE',
  payload: { bookId, returnDate },
});

export const setUserData = (userInfo) => ({
  type: 'SET_USER_DATA',
  payload: userInfo
})

export const cleanUserData = () => ({
  type: 'CLEAN_USER_DATA',
  payload: {}
})


///authors
export const getAuthors = () => async (dispatch) => {
  dispatch({ type: 'FETCH_AUTHORS_REQUEST' });

  try {
      const response = await fetchAuthors();
      dispatch({ type: 'FETCH_AUTHORS_SUCCESS', payload: response });
  } catch (error) {
      dispatch({ type: 'FETCH_AUTHORS_FAILURE', payload: error.response.data.message });
  }
};
export const getAuthorsPagin = (currentPage, pageSize) => async (dispatch) => {
  dispatch({ type: 'FETCH_AUTHORS_REQUEST' });

  try {
      const response = await fetchAuthorsPagin(currentPage, pageSize);
      dispatch({ type: 'FETCH_AUTHORS_SUCCESS', payload: response });
  } catch (error) {
      dispatch({ type: 'FETCH_AUTHORS_FAILURE', payload: error.response.data.message });
  }
};

export const addAuthorRequest = (author) => async (dispatch) =>{
  dispatch({ type: 'ADD_AUTHOR_REQUEST' });
  try {
      const response = await addAuthor(author);
      const newAuthor = { ...author, id: response }; 
      console.log('NEW AUTHOR  ', newAuthor);

      dispatch({ type: 'ADD_AUTHOR_SUCCESS', payload: newAuthor});
  } catch (error) {
      dispatch({ type: 'ADD_AUTHOR_FAILURE', payload: error.message });
  }
}
import { act } from 'react';
import { combineReducers } from 'redux';

const initialState = {
  user: null,
  loading: false,
  error: null,
};

const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'LOGIN_REQUEST':
    case 'REGISTER_REQUEST':
      return { ...state, loading: true, error: null };
    case 'LOGIN_SUCCESS':
    case 'REGISTER_SUCCESS':
      return { ...state, loading: false, user: action.payload };
    case 'LOGIN_FAILURE':
    case 'REGISTER_FAILURE':
      return { ...state, loading: false, error: action.payload };
    default:
      return state;
  }
};

const bookInitialState = {
  books: [],
  loading: false,
  error: null,
  totalPages: 0,
  currentPage: 1,
  pageSize: 10
};

const bookReducer = (state = bookInitialState, action) => {
  switch (action.type) {
      case 'FETCH_BOOKS_REQUEST':
          return { ...state, loading: true, error: null };
      case 'FETCH_BOOKS_SUCCESS':
          return {
              ...state,
              loading: false,
              books: action.payload.items, // Используйте items из ответа
              totalPages: action.payload.totalPages, // Установите общее количество страниц
              currentPage: action.payload.currentPage, // Установите текущую страницу
              pageSize: action.payload.pageSize,
          };
      case 'FETCH_BOOKS_FAILURE':
          return { ...state, loading: false, error: action.payload };
      case 'ADD_BOOK_REQUEST':
          return { ...state, loading: true, error: null };
      case 'ADD_BOOK_SUCCESS':
          return { loading: false, book: action.payload, error: null };
      case 'ADD_BOOK_FAILURE':
          return { loading: false, error: action.payload };
      case 'UPDATE_BOOK_PICK_DATE':
        return {
          ...state,
          books: state.books.map((book) =>
              book.Id === action.payload.bookId
                  ? { ...book, PickDate: new Date(), ReturnDate: payload.returnDate } // Устанавливаем дату взятия
                  : book
          ),
      };
      case 'UPDATE_BOOK_REQUEST':
      return {
        ...state,
        loading: true,
        error: null,
      };
    case 'UPDATE_BOOK_SUCCESS':
      return {
        ...state,
        loading: false,
        books: state.books.map((book) =>
          book.id === action.payload.id ? action.payload : book
        ),
      };
    case 'UPDATE_BOOK_FAILURE':
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    case 'DELETE_BOOK_REQUEST':
        return {
            ...state,
            loading: true,
            error: null,
        };
    case 'DELETE_BOOK_SUCCESS':
        return {
            ...state,
            loading: false,
            books: state.books.filter(book => book.id !== action.payload.id), // Удаляем книгу из списка
        };
    case 'DELETE_BOOK_FAILURE':
        return {
            ...state,
            loading: false,
            error: action.payload,
        };
      default:
          return state;
  }
};

const authorInitialState = {
  authors: [],
  loading: false,
  error: null,
  totalPages: 0,
  currentPage: 1,
  pageSize: 10
};

const authorsReducer = (state = authorInitialState, action) => {
  switch (action.type) {
    case 'FETCH_AUTHORS_REQUEST':
      return { ...state, loading: true, error: null };
    case 'FETCH_AUTHORS_SUCCESS':
      return { ...state, loading: false, 
        authors: action.payload 
      };
    case 'FETCH_AUTHORS_FAILURE':
      return { ...state, loading: false, error: action.payload };
    case 'ADD_AUTHOR_REQUEST':
      return {
        ...state,
        authors: [...state.authors, action.payload],
      };  
    case 'ADD_AUTHOR_SUCCESS':
          return { loading: false, author: action.payload, error: null };
    case 'ADD_AUTHOR_FAILURE':
          return { loading: false, error: action.payload };
    default:
      return state;
  }
};


const initialUserState = {
  id: '', 
  displayName: '',
  email: '',
  role: 'User',
  borrowedBooks: [], 
};

const userReducer = (state = initialUserState, action) => {
  switch (action.type) {
      case 'SET_USER_DATA':
        return{
            id: action.payload.id,
            displayName: action.payload.displayName,
            email: action.payload.email,
            role: action.payload.role,
            borrowedBooks: action.payload.books
        }
        case 'CLEAN_USER_DATA':
          return{
              id: '',
              displayName: '',
              email: '',
              borrowedBooks: []
          } 
      case 'BORROW_BOOK':
          return {
              ...state,
              borrowedBooks: [...state.borrowedBooks, action.payload],
          };
      case 'BORROW_BOOK_FAILURE':
        return{
           loading: false, error: action.payload 
        }
      case 'FETCH_USBOOKS_REQUEST':
        return { ...state, loading: true, error: null };
      case 'FETCH_USBOOKS_SUCCESS':
        console.log('acton ', action)
        return {
          ...state,
          loading: false,
          borrowedBooks: action.payload.items, // Используйте items из ответа
          totalPages: action.payload.totalPages, // Установите общее количество страниц
          currentPage: action.payload.currentPage, // Установите текущую страницу
          pageSize: action.payload.pageSize,
      };
      case 'FETCH_USBOOKS_FAILURE':
        return { ...state, loading: false, error: action.payload };
      default:
          return state;
  }
};


  const rootReducer = combineReducers({
    auth: authReducer,
    books: bookReducer, 
    users: userReducer,
    authors: authorsReducer
  });

export default rootReducer;
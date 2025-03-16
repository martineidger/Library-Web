import { Routes, Route } from 'react-router';
import React from 'react';
import HomePage from './pages/HomePage';
import UserProfilePage from './pages/UserProfilePage';
import NotFoundPage from './pages/NotFoundPage';
import AuthPage from './pages/AuthPage';
import BookPage from './pages/BookPage';
import Login from './components/Login';
import Register from './components/Registration';
import BookList from './components/BookList';
import Header from './components/Header';
import AddBookForm from './components/AddBookForm';
import EditBookForm from './components/EditBookForm';
import UserBooksPage from './pages/UserBooksPage';
import ErrorPage from './pages/ErrorPage';

import { checkToken } from './api/authApi';

function App() {
  useEffect(() => {
    checkToken();
}, []);

  return (
    <>
      <div className="wrapper">
        <Header/>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/books/add" element={<AddBookForm />} />
          <Route path="/books" element={<BookList />} />
          <Route path="/books/:bookId" element={<BookPage />} />
          <Route path="/books/edit/:bookId" element={<EditBookForm />} />
          <Route path="/userBooks" element={<UserBooksPage />} />
          <Route path="/error" element={<ErrorPage />} />
          <Route path="/auth/*" element={<AuthPage />} />
          <Route path="/login" element={<Login />} />
          <Route path="/registration" element={<Register />} />
          <Route path="/user/*" element={<UserProfilePage />} />
          
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
    </div>  
    </>
  )
}

export default App

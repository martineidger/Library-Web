import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './HomePage.css'; // Импортируем стили

const HomePage = () => {
  const navigate = useNavigate();

  let isAdmin = false
  const userRole = localStorage.getItem('userRole')
  if(userRole)
  {
      isAdmin = userRole.toLowerCase() === 'admin';
  }

  return (
    <div>
      <h1>Онлайн Библиотека</h1>
     <button onClick={() => navigate('/books')}>Books</button>
     {isAdmin && (
                <button onClick={() => navigate('/books/add')}>Add book</button>
            )}
     
    </div>
  );
};

export default HomePage;
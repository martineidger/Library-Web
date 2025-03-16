

import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { loginRequest } from '../redux/actions';
import './Login.css'; 
const Login = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { loading, error } = useSelector((state) => state.auth); 
  const userId = useSelector((state) => state.users.id); 
  const [credentials, setCredentials] = useState({ email: '', password: '' });

  useEffect(() => {
    if (userId) {
      navigate('/'); 
    }
  }, [userId, navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await dispatch(loginRequest(credentials)); 
  };

  const renderErrorMessage = () => {
    if (!error) return null;

    switch (error.status) {
      case 401:
        return <p className='error'>Неверный логин или пароль.</p>;
      case 404:
        return <p className='error'>Нет такого аккаунта.</p>;
      case 500:
        return <p className='error'>Ошибка сервера. Попробуйте позже.</p>;
      default:
        return <p className='error'>Произошла ошибка. Попробуйте еще раз.</p>;
    }
  };

  return (
    <div className='login'>
      <h2>Вход</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={credentials.email}
          onChange={handleChange}
          required
        />
        <input
          type="password"
          name="password"
          placeholder="Пароль"
          value={credentials.password}
          onChange={handleChange}
          required
        />
        <button type="submit" disabled={loading}>
          {loading ? 'Загрузка...' : 'Войти'}
        </button>
      </form>
      {renderErrorMessage()}
    </div>
  );
};

export default Login;
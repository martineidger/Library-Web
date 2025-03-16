import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { registerRequest } from '../redux/actions';
import './Register.css'; 

const Register = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { loading, error } = useSelector((state) => state.auth);
    const userId = useSelector((state) => state.users.id); // Получаем userId из состояния
    const [userData, setUserData] = useState({ email: '', password: '', displayName: '' });

     useEffect(() => {
        if (userId) {
          navigate('/'); 
        }
      }, [userId, navigate]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserData({ ...userData, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        dispatch(registerRequest(userData));
    };

    return (
        <div className='register'>
            <h2>Регистрация</h2>
            <form onSubmit={handleSubmit}>
                <input type="email" name="email" placeholder="Email" value={userData.email} onChange={handleChange} required />
                <input type="password" name="password" placeholder="Пароль" value={userData.password} onChange={handleChange} required />
                <input type="text" name="displayName" placeholder="Имя" value={userData.displayName} onChange={handleChange} required />
                <button type="submit" disabled={loading}>
                    {loading ? 'Загрузка...' : 'Зарегистрироваться'}
                </button>
            </form>
            {error && <p className='error'>{error}</p>}
        </div>
    );
};

export default Register;
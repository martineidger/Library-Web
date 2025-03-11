import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { cleanUserData } from '../redux/actions';

import './Header.css';
const Header = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    let isAdmin = false
    const userRole = localStorage.getItem('userRole')
    if(userRole)
    {
        isAdmin = userRole.toLowerCase() === 'admin';
    }
   
    const {id, displayName, books} = useSelector((state) => state.users);

    const userId = localStorage.getItem('userId')
    const userName = localStorage.getItem('userName')

    console.log(userId)
    console.log(userName)


    const handleLogout = () => {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');  
        localStorage.removeItem('userId')
        localStorage.removeItem('userName')  
        localStorage.removeItem('email')  
        dispatch(cleanUserData())
        navigate('/login'); 
    };

    return (
        <header>
            <h4>Books</h4>
            <button onClick={() => navigate('/')}>Main page</button>
            {userId ? (
                <div>
                    <p>Добро пожаловать, {userName}!</p>
                    <button onClick={handleLogout}>Выйти</button>
                </div>
            ) : (
                <div>
                    <button onClick={() => navigate('/login')}>Вход</button>
                    <button onClick={() => navigate('/registration')}>Регистрация</button>
                </div>
            )}

            {userId && !isAdmin ? (
                <div>
                    <button onClick={() => navigate('/userBooks')}>Мои книги</button>
                </div>
                ) : (
                    <div>
                    </div>
                )}

        </header>
    );
};


export default Header;
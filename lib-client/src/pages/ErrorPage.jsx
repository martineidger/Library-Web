// ErrorPage.js
import React from 'react';

const ErrorPage = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('userId');

    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <h1>Ошибка подключения</h1>
            <p>К сожалению, произошла ошибка при подключении к серверу. Пожалуйста, проверьте ваше интернет-соединение и попробуйте снова.</p>
        </div>
    );
};

export default ErrorPage;
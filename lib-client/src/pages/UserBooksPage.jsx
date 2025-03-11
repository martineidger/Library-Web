import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { getUserBooks } from '../redux/actions';
import Book from '../components/Book';

const UserBooksPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { borrowedBooks = [], loading, error, totalPages, currentPage, pageSize } = useSelector((state) => state.users);

    useEffect(() => {
        dispatch(getUserBooks(currentPage, pageSize));
    }, [dispatch]);

    const handlePageChange = (page) => {
        if (page !== currentPage) {
            dispatch(getUserBooks(page, pageSize));
        }
    };

    const handlePageSizeChange = (event) => {
        const newSize = Number(event.target.value);
        dispatch(getUserBooks(1, newSize));
    };

    if (loading) {
        return <p>Загрузка книг...</p>;
    }

    if (error) {
        return <p>Ошибка: {error}</p>;
    }

    return (
        <div>
            <h2>Список книг</h2>
            <label>
                Выберите количество объектов на странице:
                <select value={pageSize} onChange={handlePageSizeChange}>
                    <option value={5}>5</option>
                    <option value={10}>10</option>
                    <option value={20}>20</option>
                    <option value={50}>50</option>
                </select>
            </label>
            <ul>
                {borrowedBooks.length > 0 ? (
                    borrowedBooks.map((book) => (
                        <Book key={book.Id} book={book} />
                    ))
                ) : (
                    <p>Нет доступных книг.</p>
                )}
            </ul>
            <div>
                {Array.from({ length: totalPages }, (_, index) => (
                    <button
                        key={index + 1}
                        onClick={() => handlePageChange(index + 1)}
                        disabled={currentPage === index + 1}
                    >
                        {index + 1}
                    </button>
                ))}
            </div>
        </div>
    );
};

export default UserBooksPage;


import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { getBooks, getAuthors } from '../redux/actions';
import Book from './Book';
import './BookList.css';

const BookList = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { books = [], loading, error, totalPages, currentPage, pageSize } = useSelector((state) => state.books);
    const { authors = [] } = useSelector((state) => state.authors); // Получаем список авторов

    const [searchTerm, setSearchTerm] = useState('');
    const [genreFilter, setGenreFilter] = useState('');
    const [authorFilter, setAuthorFilter] = useState('');

    useEffect(() => {
        dispatch(getBooks(currentPage, pageSize));
        dispatch(getAuthors()); // Получаем список авторов
    }, [dispatch, currentPage, pageSize]);

    const handlePageChange = (page) => {
        if (page !== currentPage) {
            dispatch(getBooks(page, pageSize));
        }
    };

    const handlePageSizeChange = (event) => {
        const newSize = Number(event.target.value);
        dispatch(getBooks(1, newSize));
    };

    const filteredBooks = books.filter(book => {
        const matchesTitle = book.title.toLowerCase().includes(searchTerm.toLowerCase());
        const matchesGenre = genreFilter ? book.genre === genreFilter : true;
        
        const matchesAuthor = authorFilter ? book.authorID === authorFilter : true; // Фильтрация по AuthorID
        return matchesTitle && matchesGenre && matchesAuthor;
    });

    if (loading) {
        return <p>Загрузка книг...</p>;
    }

    if (error) {
        return <p>Ошибка: {error}</p>;
    }

    return (
        <div className="book-list">
            <h2>Список книг</h2>
            <label>
                Поиск по названию:
                <input
                    type="text"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
            </label>
            <label>
                Фильтр по жанру:
                <select value={genreFilter} onChange={(e) => setGenreFilter(e.target.value)}>
                    <option value="">Все жанры</option>
                    {/* Добавьте сюда ваши жанры */}
                    <option value="Фантастика">Фантастика</option>
                    <option value="Драма">Драма</option>
                    <option value="Приключения">Приключения</option>
                </select>
            </label>
            <label>
                Фильтр по автору:
                <select value={authorFilter} onChange={(e) => setAuthorFilter(e.target.value)}>
                    <option value="">Все авторы</option>
                    {authors.map((author) => (
                        <option key={author.id} value={author.id}>
                            {author.firstName} {author.surname}
                        </option>
                    ))}
                </select>
            </label>
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
                {filteredBooks.length > 0 ? (
                    filteredBooks.map((book) => (
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

export default BookList;
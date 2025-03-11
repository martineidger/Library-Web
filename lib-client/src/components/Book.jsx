import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { borrowBook, updateBookPickDate } from '../redux/actions';
import './Book.css'; 

const Book = ({ book }) => {

    const dispatch = useDispatch();
    const navigate = useNavigate()
    const userRole = useSelector((state) => state.users.role); 
    const isAdmin = userRole ==='Admin';


    const handleBorrow = (e) => {
        if (isAvailable) {
            e.preventDefault()
            dispatch(borrowBook(book)); 
        }
    };

    const handleBookClick = (id) =>{
        navigate(`/books/${id}`)
    }

    return (
        <div className="book" onClick={() => handleBookClick(book.id)}>
            <h2>{book.title}</h2>
            <img src={`/api/${book.imgPath}`} alt="" />
            <p><strong>ISBN:</strong> {book.isbn}</p>
            <p><strong>Genre:</strong> {book.genre}</p>
            <p><strong>Description:</strong> {book.description}</p>

            {!book.pickDate ? (
                <p>Книга в наличии</p>
            ) : (
                <p>Книга не в наличии</p>
            )}
        </div>
    );
};

const bookStyle = {
    border: '1px solid #ccc',
    borderRadius: '5px',
    padding: '10px',
    margin: '10px',
};

export default Book;
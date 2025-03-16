import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams, useNavigate } from 'react-router-dom'; // Импортируйте useParams
import { borrowBook } from '../redux/actions';
import { deleteBookRequest } from '../redux/actions';
import './BookPage.css'; // Импортируем стили
const BookPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate()
    const { bookId } = useParams(); 
    let isAdmin = false
    console.log(bookId);
    
    const books = useSelector((state) => state.books.books);
    const book = books.find(book => book.id === bookId); 

    if (!book) {
        return <p>Книга не найдена</p>; // Обработка отсутствия книги
    }

    //const userRole = useSelector((state) => state.users.role); 
    const userRole = localStorage.getItem('userRole')
    if(userRole)
    {
        isAdmin = userRole.toLowerCase() === 'admin';
    }
    
    const isAvailable = !book.pickDate; // Определите доступность книги

    const handleBorrow = () => {
        dispatch(borrowBook(book)); 
        navigate(`/books`);
    };

    const handleEditButton = () =>{
        navigate(`/books/edit/${book.id}`)
    }

    const handleDeleteButton = () =>{
        const confirmDelete = window.confirm("Вы уверены, что хотите удалить эту книгу?");
        if (confirmDelete) {
            dispatch(deleteBookRequest(book.id))
            navigate('/books')
    }
    }

    return (
        <div>
            <h2>{book.title}</h2>
            <img src={`/api/${book.imgPath}`} alt={book.title} />
            {isAdmin && 
                <p><strong>ISBN:</strong> {book.isbn}</p>}
            <p><strong>Genre:</strong> {book.genre}</p>
            <p><strong>Description:</strong> {book.description}</p>

            {isAdmin && (
                <>
                <p><strong>Pick Date:</strong> {book.pickDate ? book.pickDate.toString() : 'Available'}</p>
                <button onClick={()=>handleEditButton()}>Edit book</button>
                <button onClick={()=>handleDeleteButton()}>Delete book</button>
                </>
            )}

            {!isAdmin && (
                isAvailable ? (
                    <button onClick={() => handleBorrow()}>Взять книгу</button>
                ) : (
                    <p>Книга не в наличии</p>
                )
            )}
            
        </div>
    );
};

export default BookPage;
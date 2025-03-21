import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { updateBookRequest, getAuthors } from '../redux/actions';
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import './EditBookForm.css';

const EditBookForm = () => {
  const { bookId } = useParams();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { loading, error, books } = useSelector((state) => state.books);
  const { authors = [], loadingAuth, errorAuth } = useSelector((state) => state.authors);
  
  const [book, setBook] = useState({
    ISBN: '',
    Title: '',
    Genre: '',
    Description: '',
    AuthorID: '',
    ImgFile: null,
    ImgPath: ''
  });
  
  useEffect(() => {
    dispatch(getAuthors(1));
  }, [dispatch]);

  useEffect(() => {
    const existingBook = books.find((b) => b.id.toString() === bookId);
    if (existingBook) {
      setBook({
        
        ISBN: existingBook.isbn,
        Title: existingBook.title,
        Genre: existingBook.genre,
        Description: existingBook.description,
        AuthorID: existingBook.authorID,
        ImgFile: existingBook.imgFile || null,
        ImgPath: existingBook.imgPath
      });
    }
  }, [books, bookId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setBook((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file && file.type.startsWith('image/')) {
      setBook((prev) => ({
        ...prev,
        ImgFile: file,
      }));
    } else {
      alert('Пожалуйста, выберите файл изображения.');
      setBook((prev) => ({
        ...prev,
        ImgFile: null,
      }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Submitting book data:", book);
    console.log("Book ID:", bookId);
    
    dispatch(updateBookRequest(book, bookId));
    setBook({
      
      ISBN: '',
      Title: '',
      Genre: '',
      Description: '',
      AuthorID: '',
      ImgFile: null,
    });

     navigate(`/books`);
     window.location.reload();
  };

  const handleAuthorChange = (e) => {
    setBook((prev) => ({
      ...prev,
      AuthorID: e.target.value,
    }));
  };

  return (
    <div className="edit-book-form">
      <h2>Редактировать книгу</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="ISBN"
          placeholder="ISBN"
          value={book.ISBN}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="Title"
          placeholder="Название"
          value={book.Title}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="Genre"
          placeholder="Жанр"
          value={book.Genre}
          onChange={handleChange}
          required
        />
        <textarea
          name="Description"
          placeholder="Описание"
          value={book.Description}
          onChange={handleChange}
          required
        />
        <select name="AuthorID" value={book.AuthorID} onChange={handleAuthorChange} required>
          <option value="">Выберите автора</option>
          {authors.map((author) => (
            <option key={author.id} value={author.id}>
              {author.firstName} {author.surname}
            </option>
          ))}
        </select>
        
        {/* Image Preview */}
        {book.ImgPath && (
          <div className="image-preview">
            <h4>Текущая обложка:</h4>
            <img
              src={`/api/${book.ImgPath}`}
              alt="Preview"
              style={{ width: '100px', height: 'auto' }}
            />
          </div>
        )}

        <input
          type="file"
          name="ImgFile"
          accept="image/*"
          onChange={handleFileChange}
        />
        
        <button type="submit" disabled={loading}>
          {loading ? 'Загрузка...' : 'Обновить книгу'}
        </button>
      </form>
      {error && <p>{error}</p>}
    </div>
  );
};

export default EditBookForm;
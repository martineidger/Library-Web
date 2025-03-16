import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getAuthors, addAuthorRequest, addBookRequest } from '../redux/actions';
import './EditBookForm.css';

const AddBookForm = () => {
  const dispatch = useDispatch();
  const { authors, loading, error } = useSelector((state) => state.authors);

  const [book, setBook] = useState({
    ISBN: '',
    Title: '',
    Genre: '',
    Description: '',
    AuthorID: '',
    imgFile: null, // New state for cover image
  });

  const [isCreatingAuthor, setIsCreatingAuthor] = useState(false);
  const [newAuthor, setNewAuthor] = useState({
    firstName: '',
    surname: '',
    birthDate: '',
    country: '',
  });

  useEffect(() => {
    dispatch(getAuthors());
  }, [dispatch]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setBook((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleAuthorChange = (e) => {
    setBook((prev) => ({
      ...prev,
      AuthorID: e.target.value,
    }));
  };

  const handleNewAuthorChange = (e) => {
    const { name, value } = e.target;
    setNewAuthor((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleFileChange = (e) => {
    setBook((prev) => ({
      ...prev,
      imgFile: e.target.files[0], // Store the selected file
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log('ADDING ', book);

   

    dispatch(addBookRequest(book)); // Use FormData for file upload
    setBook({
      ISBN: '',
      Title: '',
      Genre: '',
      Description: '',
      AuthorID: '',
      imgFile: null,
    });
  };

  const handleCreateAuthor = async (e) => {
    e.preventDefault();

    if (!newAuthor.firstName || !newAuthor.surname || !newAuthor.birthDate || !newAuthor.country) {
      alert('Пожалуйста, заполните все поля для добавления автора.');
      return;
    }

    dispatch(addAuthorRequest(newAuthor));
    window.location.reload();
  };

  return (
    <div className="add-book-form">
      <h2>Добавить книгу</h2>
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
        {book.imgFile && (
          <div className="image-preview">
            <h4>Текущая обложка:</h4>
            <img
              src={URL.createObjectURL(book.imgFile)}
              alt="Preview"
              style={{ width: '100px', height: 'auto' }}
            />
          </div>
        )}
        <input
          type="file"
          accept="image/*" // Only accept image files
          onChange={handleFileChange}
          required
        />

        <button type="button" onClick={() => setIsCreatingAuthor(true)}>
          Создать нового автора
        </button>

        <button type="submit" disabled={loading}>
          {loading ? 'Загрузка...' : 'Добавить книгу'}
        </button>
      </form>

      {isCreatingAuthor && (
        <div>
          <h3>Добавить автора</h3>
          <form onSubmit={handleCreateAuthor}>
            <input
              type="text"
              name="firstName"
              placeholder="Имя"
              value={newAuthor.firstName}
              onChange={handleNewAuthorChange}
              required
            />
            <input
              type="text"
              name="surname"
              placeholder="Фамилия"
              value={newAuthor.surname}
              onChange={handleNewAuthorChange}
              required
            />
            <input
              type="date"
              name="birthDate"
              value={newAuthor.birthDate}
              onChange={handleNewAuthorChange}
              required
            />
            <input
              type="text"
              name="country"
              placeholder="Страна"
              value={newAuthor.country}
              onChange={handleNewAuthorChange}
              required
            />
            <button type="submit">
              Добавить автора
            </button>
          </form>
        </div>
      )}

      {error && <p>{error}</p>}
    </div>
  );
};

export default AddBookForm;

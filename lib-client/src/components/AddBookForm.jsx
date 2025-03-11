import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getAuthors, addAuthorRequest } from '../redux/actions';
import { addBookRequest } from '../redux/actions';
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

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log('ADDING ',book)
    dispatch(addBookRequest(book));
    setBook({
      ISBN: '',
      Title: '',
      Genre: '',
      Description: '',
      AuthorID: '',
    });
  };

  const handleCreateAuthor = async (e) => {
    e.preventDefault(); // Не забудьте предотвратить действие по умолчанию

    if (!newAuthor.firstName || !newAuthor.surname || !newAuthor.birthDate || !newAuthor.country) {
        alert('Пожалуйста, заполните все поля для добавления автора.');
        return;
    }

    dispatch(addAuthorRequest(newAuthor)); // Диспатчим действие добавления автора

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
import React, { useState } from 'react';
// import BookList from '../components/BookList';
// import SearchBar from '../components/SearchBar';
// import useBooks from '../hooks/useBooks';

const AuthPage = () => {
    console.log("home page")
//   const { books, currentPage, totalPages, searchBooks } = useBooks();
//   const [searchTerm, setSearchTerm] = useState('');

//   const handleSearchChange = (term) => {
//     setSearchTerm(term);
//     searchBooks(term);
//   };

  return (
    <div>
      <h1>Auth</h1>
      {/* <SearchBar searchTerm={searchTerm} onSearchChange={handleSearchChange} />
      <BookList books={books} currentPage={currentPage} totalPages={totalPages} /> */}
    </div>
  );
};

export default AuthPage;
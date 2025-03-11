// import { configureStore } from '@reduxjs/toolkit';
// import authReducer from './slices/authSlice.js';
// import booksReducer from './slices/booksSlice.js';

// export const store = configureStore({
//   reducer: {
//     auth: authReducer,
//     books: booksReducer,
//   },
// });

// src/store.js
import { configureStore } from '@reduxjs/toolkit';
import rootReducer from './reducers'; // Создайте файл reducers.js

export const store = configureStore({reducer: rootReducer});
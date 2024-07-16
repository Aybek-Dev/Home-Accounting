import React, { createContext } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import UserAccounting from './accounting/userAccounting';

export const Context = createContext(null);

const userStore = new UserAccounting(); // Создание экземпляра

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Context.Provider value={{ user: userStore }}> {/* Использование экземпляра */}
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Context.Provider>
);

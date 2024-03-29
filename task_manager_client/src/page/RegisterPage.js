import React, { useState } from 'react';
import { Link } from 'react-router-dom';

// Это ваш компонент страницы регистрации
function RegisterPage() {
  // Создайте состояния для хранения введенных данных
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [phone, setPhone] = useState('');
  const [language, setLanguage] = useState('ru'); // Создайте состояние для хранения выбранного языка// Создайте состояние для хранения текущей темы

  // Создайте функцию для обработки отправки формы
  const handleSubmit = (e) => {
    // Предотвратите перезагрузку страницы
    e.preventDefault();
    // Проверьте, совпадают ли пароли
    if (password !== confirmPassword) {
      alert('Пароли не совпадают');
      return;
    }
    // Выполните логику регистрации здесь
    // Например, отправьте данные на сервер или сохраните в локальное хранилище
    console.log('Регистрация успешна');
  };

  // Верните JSX-код для отображения формы регистрации
  return (
    <div className={`shadow-box container`}>
      <h1 className="title">Регистрация</h1>
      <form onSubmit={handleSubmit} className="form">
        <div className="inputGroup">
          <label htmlFor="first-name">
            Имя:
          </label>
          <input
            type="text"
            id="first-name"
            name="first-name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />
        </div>
        <div className="inputGroup">
          <label htmlFor="last-name">
            Фамилия:
          </label>
          <input
            type="text"
            id="last-name"
            name="last-name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />
        </div>
        <div className="inputGroup">
          <label htmlFor="email">
            Email:
          </label>
          <input
            type="email"
            id="email"
            name="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="inputGroup">
          <label htmlFor="password">
            Пароль:
          </label>
          <input
            type="password"
            id="password"
            name="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="inputGroup">
          <label htmlFor="confirm-password">
            Повторите пароль:
          </label>
          <input
            type="password"
            id="confirm-password"
            name="confirm-password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
          />
        </div>
        <div className="inputGroup">
          <label htmlFor="phone">
            Телефон:
          </label>
          <input
            type="tel"
            id="phone"
            name="phone"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
            required
          />
        </div>
        <button type="submit">
          Зарегистрироваться
        </button>
      </form>

      <p>
        Уже зарегистрированы? <Link className='link' to="/login">Войти</Link>
      </p>
    </div>
  );
}

export default RegisterPage;

import React, { useEffect, useState } from 'react';
import styles from './Register.module.css'; // Импортируйте файл стилей

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

  // Создайте функцию для обработки изменения языка
  const handleLanguageChange = (e) => {
    // Получите значение выбранного языка
    const value = e.target.value;
    // Обновите состояние языка
    setLanguage(value);
  };

  // Создайте объект с текстами на разных языках
  const texts = {
    ru: {
      title: 'Регистрация',
      firstName: 'Имя',
      lastName: 'Фамилия',
      email: 'Email',
      password: 'Пароль',
      confirmPassword: 'Повторите пароль',
      phone: 'Телефон',
      submit: 'Зарегистрироваться',
      language: 'Язык',
    },
    en: {
      title: 'Register',
      firstName: 'First name',
      lastName: 'Last name',
      email: 'Email',
      password: 'Password',
      confirmPassword: 'Confirm password',
      phone: 'Phone',
      submit: 'Register',
      language: 'Language',
    },
    de: {
      title: 'Registrieren',
      firstName: 'Vorname',
      lastName: 'Nachname',
      email: 'Email',
      password: 'Passwort',
      confirmPassword: 'Passwort bestätigen',
      phone: 'Telefon',
      submit: 'Registrieren',
      language: 'Sprache',
    },
  };
  useEffect(() => {

    async function Deere() {
      const response = await fetch("http://localhost:5023/api/Tests", {
        method: 'GET',
        headers: {
          'Content-type': 'application/json',
        },
        'mode':'no-cors'
      });

      console.log(response)
    }
    Deere();
  }, []);
  // Верните JSX-код для отображения формы регистрации
  return (
    <div className={`shadow-box ${styles.container}`}>
      <h1 className={styles.title}>{texts[language].title}</h1>
      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.inputGroup}>
          <label htmlFor="first-name" className={styles.label}>
            {texts[language].firstName}:
          </label>
          <input
            type="text"
            id="first-name"
            name="first-name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="last-name" className={styles.label}>
            {texts[language].lastName}:
          </label>
          <input
            type="text"
            id="last-name"
            name="last-name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="email" className={styles.label}>
            {texts[language].email}:
          </label>
          <input
            type="email"
            id="email"
            name="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="password" className={styles.label}>
            {texts[language].password}:
          </label>
          <input
            type="password"
            id="password"
            name="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="confirm-password" className={styles.label}>
            {texts[language].confirmPassword}:
          </label>
          <input
            type="password"
            id="confirm-password"
            name="confirm-password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="phone" className={styles.label}>
            {texts[language].phone}:
          </label>
          <input
            type="tel"
            id="phone"
            name="phone"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
            required
            className={styles.input}
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="language" className={styles.label}>
            {texts[language].language}:
          </label>
          <select
            id="language"
            name="language"
            value={language}
            onChange={handleLanguageChange}
            className={styles.select}
          >
            <option value="ru">Русский</option>
            <option value="en">English</option>
            <option value="de">Deutsch</option>
          </select>
        </div>
        <button type="submit" className={styles.button}>
          {texts[language].submit}
        </button>
      </form>
    </div>
  );
}

export default RegisterPage;

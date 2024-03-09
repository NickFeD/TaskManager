import { Button, Field, Input, Link, Select } from '@fluentui/react-components';
import React, { useState } from 'react';

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
  // Верните JSX-код для отображения формы регистрации
  return (
    <div className=" container shadow-box login-page">
      <h1>{texts[language].title}</h1>
      <form onSubmit={handleSubmit} className="form">

        <Field
          label={{ htmlFor: "first-name", children: `${texts[language].firstName}` }}>
          <Input
            type="text"
            id="first-name"
            name="first-name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "last-name", children: `${texts[language].lastName}` }}>
          <Input
            type="text"
            id="last-name"
            name="last-name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "email", children: `${texts[language].email}` }}>
          <Input
            type="email"
            id="email"
            name="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "password", children: `${texts[language].password}` }}>
          <Input
            type="password"
            id="password"
            name="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "confirm-password", children: `${texts[language].confirmPassword}` }}>
          <Input
            type="password"
            id="confirm-password"
            name="confirm-password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "phone", children: `${texts[language].phone}` }}>
          <Input
            type="tel"
            id="phone"
            name="phone"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "language", children: `${texts[language].language}` }}
        >
          <Select
            id="language"
            name="language"
            value={language}
            onChange={handleLanguageChange}
          >
            <option value="ru">Русский</option>
            <option value="en">English</option>
            <option value="de">Deutsch</option>
          </Select>
        </Field>

        <Button type="submit" appearance='primary'>
          {texts[language].submit}
        </Button>
      </form>

      <p>
        Уже зарегистрированы? <Link href="/login">Войти</Link>
      </p>
    </div>
  );
}

export default RegisterPage;

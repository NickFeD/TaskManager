import { Button, Field, Input, Link } from "@fluentui/react-components";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

function LoginPage() {
  // Создаем состояния для электронной почты, пароля и ошибки
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [emailError, setEmailError] = useState("")
  const [isValidEmail, setIsValidEmail] = useState(false);
  const [isValidPassword, setIsValidPassword] = useState(false);


  // Получаем доступ к истории браузера
  const history = useNavigate();

  const validationEmail = (email) => {
    if (/^[ ]*([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})[ ]*$/i.test(email))
      return true;
    return false;
  }
  // Очищаем ошибку при изменении электронной почты или пароля
  const changePassword = (newPassword) => {
    setPassword(newPassword)
    const valid = !(!newPassword)
    setIsValidPassword(valid)
  }

  const changeEmail = (newEmail) => {
    setEmail(newEmail)
    const valid = validationEmail(newEmail)
    setIsValidEmail(valid)
    setEmailError(valid ? "" : "Пожалуйста, введите корректную электронную почту");
  }
  // Определяем функцию для отправки данных на сервер
  const handleSubmit = async (e) => {
    // Предотвращаем перезагрузку страницы
    e.preventDefault();
    // Отправляем запрос на сервер с помощью fetch или axios
    try {
      // Здесь вы можете использовать свой URL и метод для аутентификации
      const response = await fetch("/api/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });
      // Проверяем статус ответа
      if (response.ok) {
        // Получаем токен из ответа
        const data = await response.json();
        const token = data.token;
        // Сохраняем токен в localStorage
        localStorage.setItem("token", token);
        // Перенаправляем пользователя на главную страницу
        history.push("/");
      } else {
        // Получаем сообщение об ошибке из ответа
        const data = await response.json();
        const message = data.message;
        // Устанавливаем состояние ошибки
        setError(message);
      }
    } catch (err) {
      // Обрабатываем ошибку сети
      setError("Произошла ошибка при подключении к серверу");
    }
  };

  // Возвращаем JSX разметку для отображения формы
  return (
    <div className=" container shadow-box login-page">
      <h1 className="title">Вход на сайт</h1>
      <form className="form" onSubmit={handleSubmit}>
        <Field
          label={{ htmlFor: "email", children: "Электронная почта" }}
          validationMessage={emailError}>
          <Input
            className="input"
            type="email"
            id="email"
            value={email}
            onChange={(e) => changeEmail(e.target.value)}
          />
        </Field>

        <Field
          label={{ htmlFor: "password", children: "Пароль" }}>
          <Input
            className="input"
            type="password"
            id="password"
            value={password}
            onChange={(e) => changePassword(e.target.value)}
          />
        </Field>

        <Button className="button" appearance="primary" type="submit" disabled={!(isValidEmail && isValidPassword)}>Войти</Button>
      </form>
      {error && <p className="error">{error}</p>}
      <p>
        Еще не зарегистрированы? <Link href="/registration"  >Создать аккаунт</Link>
      </p>
    </div>
  );
}

export default LoginPage;

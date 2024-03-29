import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles.css';
import reportWebVitals from './reportWebVitals';
import ThemeProvider from './providers/ThemeProvider'
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Root from './routes/root';
import RegisterPage from './page/RegisterPage';
import LoginPage from './page/LoginPage';
import Main from './routes/main'

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root />,
        //loader: rootLoader,
        children: [
            { index: true, element: <Main />, },
        ],
    },
    {
        path: "login",
        element: <LoginPage />,
        //loader: teamLoader,
    },
    {
        path: "registration",
        element: <RegisterPage />,
        //loader: teamLoader,
    },
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <ThemeProvider>
            <RouterProvider router={router} />
        </ThemeProvider>
    </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

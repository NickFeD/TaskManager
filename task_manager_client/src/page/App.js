import '../styles.css';
import { Route, Routes, Link } from 'react-router-dom';
import RegisterPage from './RegisterPage';
import LoginPage from './LoginPage';
import Root from '../routes/root';

function App() {

  const metod = async () => {
    const response = await fetch("/api/Tests", {
      method: "GET",
      headers: {
        "Content-Type": "application/json"
      }
    })
    if (response.ok) {
      var responseJson = await response.json();
      console.log(responseJson)
    }
  }

  metod();
  return (
    <>
      <Routes>
        <Route path="/registration" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path='/' element={<Root />} />
      </Routes>
    </>
  );
}

export default App;

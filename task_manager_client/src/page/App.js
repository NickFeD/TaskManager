import RegisterPage from './RegisterPage';
import LoginPage from './LoginPage';
import { Route, Routes } from 'react-router-dom';
import MainPage from './MainPage';
import { Link } from '@fluentui/react-components';

function App() {
  return (
    <div>
      <nav>
        <ul>
          <li><Link href="/registration">RegisterPage</Link></li>
          <li><Link href="/login">LoginPage</Link></li>
          <li><Link href="/">MainPage</Link></li>
        </ul>
      </nav>
      <Routes>
        <Route path="/registration" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/" element={<MainPage />} />
      </Routes>
    </div>
  );
}

export default App;

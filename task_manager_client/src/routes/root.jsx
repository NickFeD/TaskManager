import { Link, Outlet } from "react-router-dom";
import styles from "./root.module.css"

export default function Root() {
    return (
        <>
            <nav className={styles.navbar}>
                <Link className="link header-logo" to="/">Logo</Link>
                <ul className={styles.nav_list}>
                    <li><Link className="link" to="#">Проекты</Link></li>
                    <li><Link className="link" to="#">Доски</Link></li>
                    <li><Link className="link" to="#">Задача</Link></li>
                </ul>
                <Link className="link" to="/login">Войти</Link>
            </nav>
            <div id="detail">
                < Outlet />
            </div>
        </>
    );
}
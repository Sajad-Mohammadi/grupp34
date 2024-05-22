import './Navbar.css'
import { NavLink } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import { authService } from '../../Services/authService';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../utils/UserContext';


const Navbar = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const { changeUser, retrieveUser } = useContext(UserContext);
    const navigate = useNavigate();

    useEffect(() => {
        authService.isLoggedIn() ? setIsLoggedIn(true) : setIsLoggedIn(false);
    });

    const logout = () => {
        authService.logout();
        changeUser(null);
        setIsLoggedIn(false);
        navigate('/');
    }

    return (
        <div className="nav">
            <ul className="nav-menu">
                <li className="nav-item nav-logo">
                    <NavLink to="/" className="nav-link">Karriärportalen</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink to="/recruiters" className="nav-link">Recruiters</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink to="/candidates" className="nav-link">Career Candidates</NavLink>
                </li>
            </ul>

            {!isLoggedIn
                ? <ul className="nav-menu">
                    <li className="nav-item">
                        <NavLink to="/register" className="nav-link nav-register">Register</NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink to="/login" className="nav-link nav-login">Login</NavLink>
                    </li>
                </ul>
                : <ul className="nav-menu">
                    <li className="nav-item">
                        <button onClick={logout} className="nav-link btn-logout"><span>Logout</span></button>
                    </li>
                    <li className="nav-item">
                        <NavLink to="/profile" className="nav-link nav-profile">Profile</NavLink>
                    </li>
                </ul>
            }

        </div>
    );
};

export default Navbar
import React from 'react';
import { NavLink } from "react-router-dom";
import './Footer.css';

const Footer = () => {
    return (
        <footer>
            <div className="footer-content">
                <p>&copy; 2024 Karriärportalen</p>
                <ul className="footer-menu">
                    <li className='foot-link'>
                        <NavLink to="/" className="footer-item">Karriärportalen</NavLink>
                    </li>
                    <li className='foot-link'>
                        <NavLink to="/recruiters" className="footer-item" >Recruiters</NavLink>
                    </li>
                    <li className='foot-link'>
                        <NavLink to="/candidates" className="footer-item" >Career Candidates</NavLink>
                    </li>
                </ul>
            </div>
        </footer>
    );
};

export default Footer;
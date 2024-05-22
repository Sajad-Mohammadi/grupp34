import React from 'react';
import './ContactEmail.css';

const ContactEmail = (email) => {
    const emailAddress = email;

    const handleContactClick = () => {
        window.location.href = `mailto:${emailAddress}`;
    };

    return (
        <button className='btn-email' onClick={handleContactClick}>
            Email me
        </button>
    );
};

export default ContactEmail;

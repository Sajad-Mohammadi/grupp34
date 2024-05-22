import React from 'react';
import './ContactPhone.css';

const ContactPhone = (phoneNumber) => {
    const number = phoneNumber;

    const handleContactClick = () => {
        window.location.href = `tel:${number}`;
    };

    return (
        <button className='btn-call' onClick={handleContactClick}>
            Call me
        </button>
    );
};

export default ContactPhone;

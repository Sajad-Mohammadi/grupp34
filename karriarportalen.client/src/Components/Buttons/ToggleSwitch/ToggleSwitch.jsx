
import React, { useState } from 'react';
import './ToggleSwitch.css';

const ToggleSwitch = ({ isChecked, handleToggle }) => {
    return (
        <label className="toggle-switch">
            <input type="checkbox" checked={isChecked} onChange={handleToggle} />
            <span className="slider round"></span>
        </label>
    );
};

export default ToggleSwitch;

// src/SearchBar.js
import React, { useState } from 'react';
import './SearchBar.css';

const SearchBar = ({ onSearch }) => {
    const [query, setQuery] = useState('');

    const handleInputChange = (event) => {
        setQuery(event.target.value);
    };

    const handleSearch = () => {
        onSearch(query);
    };

    return (
        <div className='searchBar'>
            <input
                type="text"
                value={query}
                onChange={handleInputChange}
                placeholder="Search..."
            />
            <button className='btn-search' onClick={handleSearch}>Search</button>
        </div>
    );
};

export default SearchBar;

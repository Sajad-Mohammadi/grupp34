import axios from 'axios';
import { jwtDecode } from 'jwt-decode';


const setToken = (token, rememberme) => {
    if (rememberme) {
        localStorage.setItem('token', token);
    } else {
        sessionStorage.setItem('token', token);
    }
}


const getToken = () => {
    if (sessionStorage.getItem('token')) {
        return sessionStorage.getItem('token');
    } else if (localStorage.getItem('token')) {
        return localStorage.getItem('token');
    } else {
        return null;
    }
}


const setUserID = (userID, rememberme) => {
    if (rememberme) {
        localStorage.setItem('userID', userID);
    } else {
        sessionStorage.setItem('userID', userID);
    }
}


const getUserId = () => {
    if (sessionStorage.getItem('userID')) {
        return sessionStorage.getItem('userID');
    } else if (localStorage.getItem('userID')) {
        return localStorage.getItem('userID');
    } else {
        return null;
    }
}

const isLoggedIn = () => {
    const token = getToken();
    if (token) {
        const decodedToken = jwtDecode(token);
        return decodedToken.exp * 1000 > Date.now();
    }
    return false;
}


const login = async (formData) => {
    try {
        const response = await axios.post("account/login", formData, {
            headers: {
                "Content-Type": "application/json",
            },
        });
        return response;
    } catch (error) {
        return error;
    }
}


const register = async (formData) => {
    try {
        const response = await axios.post("account/register", formData);
        return response;
    } catch (error) {
        return error;
    }
}


const logout = () => {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('userID');
    sessionStorage.removeItem('userEmail');
    sessionStorage.removeItem('userRole');

    localStorage.removeItem('token');
    localStorage.removeItem('userID');
    localStorage.removeItem('userEmail');
    localStorage.removeItem('userRole');

    return "logged out successfully"
}


const getUser = async () => {
    if (!isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.get("account/getCurrentUser", {
                headers: {
                    'Authorization': `Bearer ${getToken()}`
                }
            });
            return response.data;
        } catch (error) {
            return error;
        }
    }
}


export const authService = {
    setToken,
    getToken,
    setUserID,
    getUserId,
    login,
    register,
    isLoggedIn,
    getUser,
    logout,
};
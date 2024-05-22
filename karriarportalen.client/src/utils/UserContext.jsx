import React, { useContext, useState } from "react";
import { jwtDecode } from 'jwt-decode';

export const UserContext = React.createContext();

export const useUserContext = () => {
    return useContext(UserContext);
}

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState({});

    const changeUser = (registredUser) => {
        if (!registredUser) {
            setUser({});
            return;
        }
        setUser({ ...user, ...registredUser });
    }

    const retrieveUser = () => {
        return user;
    }

    const isTokenValid = () => {
        if (user.token) {
            const decodedToken = jwtDecode(user.token);
            return decodedToken.exp * 1000 > Date.now();
        }
        return false;
    }

    return (
        <UserContext.Provider value={{ changeUser, retrieveUser, isTokenValid }}>
            {children}
        </UserContext.Provider>
    );
}
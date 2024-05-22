import { authService } from "../Services/authService";
import { Navigate } from "react-router-dom";


export const PrivateRoute = ({ children, ...rest }) => {

    return authService.isLoggedIn() ? children : <Navigate to="/login" />
}
import { useState, useContext } from "react";
import { useNavigate } from 'react-router-dom';
import { authService } from '../../Services/authService.jsx';
import { UserContext } from "../../utils/UserContext.jsx";
import "./Login.css";
import ToggleSwitch from "../../Components/Buttons/ToggleSwitch/ToggleSwitch.jsx";


const Login = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
        rememberme: false,
    });
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const navigate = useNavigate();
    const { changeUser } = useContext(UserContext);

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]:
                e.target.type === "checkbox" ? e.target.checked : e.target.value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError(null);
        setIsLoading(true);
        const response = await authService.login(formData);
        setIsLoading(false);

        if (response?.status === 200) {
            console.log(response.data);

            authService.setToken(response.data.token, formData.rememberme);
            authService.setUserID(response.data.userID, formData.rememberme);

            changeUser(response.data);
            navigate('/profile');
        } else if (response?.response?.status === 401) {
            const errors = response.response.data.errors;
            setError({ errors: errors });
        } else if (response?.response?.status === 500) {
            setError({ errors: "Internal Server Error" });
        }
        else {
            setError(response.response.data.errors);
        }
    };

    return (
        <div className="login-container">
            <form onSubmit={handleSubmit}>
                <div id="login-inputs">

                    <div>
                        <label htmlFor="email">Email</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                        />
                        {error && <p className="text-error">{error.Email}</p>}
                    </div>
                    <div>
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                        />
                        {error && <p className="text-error">{error.Password}</p>}
                    </div>
                    <div>
                        <label htmlFor="rememberme">Remember me</label>
                        <input
                            type="checkbox"
                            name="rememberme"
                            value={formData.rememberme}
                            onChange={handleChange}
                        />
                    </div>
                </div>
                <button type="submit" className="btn-login">Login</button>
                {isLoading && <p>Loading...</p>}
                {error && <p className="text-error">{error.errors}</p>}
            </form>
        </div>
    );
};

export default Login;

import React, { useState, useContext } from "react";
import { authService } from "../../Services/authService.jsx";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../utils/UserContext.jsx";
import ToggleSwitch from "../../Components/Buttons/ToggleSwitch/ToggleSwitch.jsx";
import "./Register.css";

const Register = () => {
    const [formData, setFormData] = useState({
        firstname: "",
        lastname: "",
        email: "",
        password: "",
        confirmPassword: "",
        userRole: "JobSeeker",
    });
    const navigate = useNavigate();
    const { changeUser } = useContext(UserContext);

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const [isEmployerChecked, setIsEmplyerChecked] = useState(false);

    const handleRoleToggle = () => {
        setIsEmplyerChecked(!isEmployerChecked);
        setFormData({
            ...formData,
            userRole: isEmployerChecked ? "JobSeeker" : "JobSeeker",
        });
    }

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError(null);
        setIsLoading(true);
        const response = await authService.register(formData);
        setIsLoading(false);
        console.log(response);

        if (response?.status === 200) {
            authService.setToken(response.data.token);
            authService.setUserID(response.data.userID);

            changeUser(response.data);

            navigate("/profile");
        } else if (response?.response?.status === 401) {
            const errors = response.response.data.title;
            setError({ error: errors });
        } else if (response?.response?.status === 400) {
            if (response.response.data.status === 400) {
                const errors = response.response.data.errors;
                setError({ error: errors });
            } else {
                const errors = response.response.data;
                setError({ error: errors });
            }
        } else if (response?.response?.status === 500) {
            setError({ error: "Internal Server Error" });
        } else {
            setError({ error: response.response.data });
        }
    };

    return (
        <div className="register-container">
            <form className="form-register" onSubmit={handleSubmit}>
                <div id="form-inputs">
                    <div>
                        <label htmlFor="firstname">Firstname</label>
                        <input
                            type="text"
                            name="firstname"
                            value={formData.firstname}
                            onChange={handleChange}
                        />
                        {error?.error?.Firstname && <p className="text-error">{error.error.Firstname[0]}</p>}
                    </div>
                    <div>
                        <label htmlFor="lastname">Lastname</label>
                        <input
                            type="text"
                            name="lastname"
                            value={formData.lastname}
                            onChange={handleChange}
                        />
                        {error?.error?.Lastname && <p className="text-error">{error.error.Lastname[0]}</p>}
                    </div>
                    <div>
                        <label htmlFor="email">Email</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                        />
                        {error?.error?.Email && <p className="text-error">{error.error.Email[0]}</p>}
                    </div>
                    <div>
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                        />
                        {error?.error?.Password && <p className="text-error">{error.error.Password}</p>}
                    </div>
                    <div>
                        <label htmlFor="confirmPassword">Confirm password</label>
                        <input
                            type="password"
                            name="confirmPassword"
                            value={formData.confirmPassword}
                            onChange={handleChange}
                        />
                        {error?.error?.ConfirmPassword && <p className="text-error">{error.error.ConfirmPassword[0]}</p>}
                    </div>
                </div>

                <div className="register-role">
                    <label className={!isEmployerChecked ? 'toggle-on' : 'toggle-off'} htmlFor="role">Seeking Employment Opportunities</label>
                    <ToggleSwitch isChecked={isEmployerChecked} handleToggle={handleRoleToggle} />
                    <label className={isEmployerChecked ? 'toggle-on' : 'toggle-off'} htmlFor="role">Seeking Employment Candidates</label>

                    <p>{isEmployerChecked ? "The profile you're creating will adjust to fit an employer's requirements." : "The profile you're creating will evolve to suit your job-seeking needs."}</p>
                </div>


                <div>
                    <button type="submit" className="btn-register">Register</button>
                </div>
                {isLoading && <p>Loading...</p>}
                {error?.error?.message && <p className="text-error">{error.error.message}</p>}
            </form>
        </div>
    );
};

export default Register;

import './App.css';
import { Routes, Route } from 'react-router-dom';
import { useEffect, useContext, useState } from 'react';

import Home from './Pages/Home/Home';
import Recruiters from './Pages/Recruiters/Recruiters';
import Candidates from './Pages/Candidates/Candidates';
import Login from './Pages/Login/Login';
import Register from './Pages/Register/Register';
import Profile from './Pages/Profile/Profile'
import Navbar from './Components/Navbar/Navbav';
import { PrivateRoute } from './Routes/PrivateRoute';
import { UserContext } from './utils/UserContext';
import { authService } from './Services/authService';
import { CVProvider } from './utils/CvContext';
import Footer from './Components/Footer/Footer';


function App() {
    const [isLoading, setIsLoading] = useState(true);
    const { changeUser, retrieveUser } = useContext(UserContext);
    const { getUser } = authService;

    useEffect(() => {
        async function getUserData() {
            if (authService.isLoggedIn()) {
                const user = await getUser();
                console.log(user);
                changeUser(user);
                setIsLoading(false);
            } else {
                console.log('user is not logged in');
                setIsLoading(false);
            }
        }
        getUserData();
    }, []);

    if (isLoading) {
        return (
            <div className='isLoading'>
                <h1>Loading...</h1>
            </div>
        );
    }

    return (
        <>
            <div className="header">
                <Navbar />
            </div>
            <div className='main'>

                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/recruiters" element={<Recruiters />} />
                    <Route path="/candidates" element={<Candidates />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/profile" element=
                        {
                            <CVProvider>
                                <PrivateRoute>
                                    <Profile />
                                </PrivateRoute>
                            </CVProvider>
                        }
                    />
                    <Route path="*" element={<h1>Not Found</h1>} />
                </Routes>
            </div>
            <div>
                <Footer />
            </div>
        </>
    );
}

export default App;
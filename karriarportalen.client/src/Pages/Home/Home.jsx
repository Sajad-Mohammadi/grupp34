import './Home.css';
import { useEffect, useState } from 'react';

const Home = () => {
    const [forecasts, setForecasts] = useState();
    useEffect(() => {
        populateWeatherData();
    }, []);


    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
        : <h3>Welcome</h3>;

    return (
        <div className='home-page-content'>
            {contents}
            <button className='btn-get-started' onClick={() => window.location.href = '/register'}>GET STARTED</button>
        </div>
    )

    async function populateWeatherData() {
        let token = sessionStorage.getItem('token');

        const response = await fetch('weatherforecast', {
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });
        const data = await response.json();
        setForecasts(data);
    }
}

export default Home;
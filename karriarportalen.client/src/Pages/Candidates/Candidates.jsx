import { useState, useEffect } from "react";
import { cvService } from "../../Services/cvService";
import trimString from "../../utils/trimString";
import './Candidates.css';
import SearchBar from "../../Components/SearchBar/SearchBar";

const Candidates = () => {
    const [cvs, setCvs] = useState({});
    const [loading, setLoading] = useState(true);
    const { getAllCVs } = cvService;

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async (searchQuery) => {
        setLoading(true);
        try {
            const respond = await getAllCVs(searchQuery);
            const trimmedCvs = respond.map((cv) => {
                cv.shortBio = trimString(cv.shortBio, 100);

                if (cv.educations.length > 1) {
                    const lastEdu = cv.educations.slice(-1);
                    cv.educations = lastEdu;
                }
                if (cv.experiences.length > 1) {
                    const lastExp = cv.experiences.slice(-1);
                    cv.experiences = lastExp;
                }
                return cv;
            });
            console.log(trimmedCvs);
            setCvs(trimmedCvs);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };








    if (loading) {
        return <h1>Loading...</h1>;
    }

    return (
        <div>
            <div className="div-search">
                <SearchBar onSearch={fetchData} />
            </div>
            <div className="container">
                {cvs.length === 0 ? <h1>No CVs found</h1> : cvs.map((cv) => (
                    <div key={cv.appUserId} className="can-cv">
                        <div className="can-header">
                            <img src="src/assets/pp2.png" alt="User Avatar" className="can-avatar" />
                            <div className="can-name-loc">
                                <h4>{cv.firstname} {cv.lastname}</h4>
                                {(cv.city || cv.country) ? <p className="can-location">⟟ {cv.city} {cv.country}</p> : ""}
                            </div>
                        </div>
                        <p className="can-shortBio">{cv.shortBio}</p>
                        <div className="can-edu-exp">
                            {cv.educations.length > 0 &&
                                <div>
                                    <p>{cv.educations ? cv.educations[0].title : ""}</p>
                                </div>
                            }
                            {cv.experiences.length > 0 &&
                                <div>
                                    <p>{cv.experiences ? cv.experiences[0].title : ""}</p>
                                </div>
                            }
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Candidates;
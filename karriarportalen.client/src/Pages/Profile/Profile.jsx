
import './Profile.css';
import { useEffect, useState, useContext } from "react";
import { UserContext } from "../../utils/UserContext.jsx";
import { useCvContext } from '../../utils/CvContext.jsx';
import ContactEmail from '../../Components/Buttons/Email/ContactEmail.jsx';
import ContactPhone from '../../Components/Buttons/Call/ContactPhone.jsx';
import EditProfile from './EditProfile/EditProfile.jsx';
import trimString from '../../utils/trimString.jsx';

const Profile = () => {

    const [editMode, setEditMode] = useState(false);

    const [user, setUser] = useState({});
    const [fullName, setFullName] = useState('');
    const [loading, setLoading] = useState(true);

    const { retrieveUser } = useContext(UserContext);

    const { retrieveCV, retrieveCVSkill, isLoading } = useCvContext();
    const cvData = retrieveCV();
    const cvSkillData = retrieveCVSkill();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const user = retrieveUser();
                setUser(user);
                setFullName(`${user.firstname} ${user.lastname}`);
                setLoading(false);
            } catch (error) {
                console.error(error);
                setLoading(false);
            }
        };

        fetchData();
        console.log(cvData);
    }, [cvData, user]);

    useEffect(() => {
        if (user !== undefined) {
            setFullName(
                trimString(`${user.firstname} ${user.lastname}`, 16)
            );
        }
    }, []);





    if (loading || isLoading) {
        return <h1>Loading...</h1>;
    }

    if (editMode) {
        return (
            <EditProfile setEditMode={setEditMode} />
        );
    }

    return (
        <div className="profile-page-container">
            <div className="aside-panel">
                <div className='aside-panel-chid'>
                    <img src="src/assets/pp2.png" alt="User Avatar" className="profile-avatar" />
                    <h3>{fullName}</h3>
                    <p className='location'>⟟ {cvData.city}</p>
                    <ContactEmail email={cvData.email} />
                    <ContactPhone phoneNumber={cvData.phoneNumber} />
                    <a href={cvData.website} target="_blank" rel="noopener noreferrer" className='website'><span>website</span></a>
                    <div className='public-profile'>
                        {cvData.isPublic
                            ? <p className='public'>Your profile is visible to everyone.</p>
                            : <p className='private'>Your profile is visible only to logged in users.</p>}
                    </div>
                </div>
                <button onClick={() => setEditMode(true)} className='btn-edit'>Edit</button>
            </div>
            <div className="main-content">
                <div>
                    <div className='div-title'>
                        <h3>Short bio:</h3>
                    </div>
                    <div className='div-part'>
                        <p className='shortBio'>{cvData.shortBio}</p>
                        {cvSkillData.length > 0 &&
                            <div>
                                <h3 className='skills-title'>Skills:</h3>
                                <div className='skills'>
                                    {/* <input type="button" value="skills:" className='btn-skill' /> */}
                                    {cvSkillData.map((skill) => (
                                        <input type="button" key={skill.id} className='btn-skill' value={skill.name} />
                                    ))}
                                </div>
                            </div>
                        }
                    </div>
                    {cvData.experiences.length > 0 &&
                        <div>
                            <div className='div-title'>
                                <h3>Experiences:</h3>
                            </div>
                            <div className='div-part'>
                                {cvData.experiences.map((experience) => (
                                    <div key={experience.id} className='div-part-child'>
                                        <h3>{experience.title}</h3>
                                        <h5>{experience.startDate + " - "}{experience.endDate}</h5>
                                        <div>
                                            <p>{experience.description}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    }
                    {cvData.educations.length > 0 &&
                        <div>
                            <div className='div-title'>
                                <h3>Educations:</h3>
                            </div>
                            <div className='div-part'>
                                {cvData.educations.map((education) => (
                                    <div key={education.id} className='div-part-child'>
                                        <h3>{education.title}</h3>
                                        <h5>{education.startDate + " - "} {education.endDate}</h5>
                                        <div>
                                            <p>{education.description}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    }
                </div>
            </div>
        </div>
    );
};

export default Profile;

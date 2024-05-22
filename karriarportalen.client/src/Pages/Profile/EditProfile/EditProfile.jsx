
import './EditProfile.css';
import dateFormat from 'dateformat';
import { useEffect, useState, useContext, useRef } from "react";
import { UserContext } from "../../../utils/UserContext.jsx";
import { useCvContext } from '../../../utils/CvContext.jsx';
import { cvService } from '../../../Services/cvService.jsx';
import PopupAdd from '../../../Components/windows/Popup/PopupAdd.jsx';
import trimString from '../../../utils/trimString.jsx';

const EditProfile = () => {

    const { retrieveUser } = useContext(UserContext);
    const { fetchCvData, retrieveCV, retrieveCVSkill, isLoading } = useCvContext();
    const cv = retrieveCV();
    const cvSkillData = retrieveCVSkill();
    const { updateCV, updateEducation, updateExperience, deleteEducation, deleteExperience } = cvService;

    const [editMode, setEditMode] = useState(true);
    const [showPopup, setShowPopup] = useState(false);
    const [createFuction, setCreateFunction] = useState({});

    const [user, setUser] = useState({});
    const [cvData, setCvData] = useState(cv);
    const [newCvData, setNewCvData] = useState({});
    const [isPublic, setIsPublic] = useState();
    const [fullName, setFullName] = useState('');
    const [newEducationData, setNewEducationData] = useState({});
    const [newExperienceData, setNewExperienceData] = useState({});
    const [loading, setLoading] = useState(true);

    const prevEducationsRef = useRef(cvData.educations);
    const prevExperiencesRef = useRef(cvData.experiences);






    useEffect(() => {
        const fetchData = async () => {
            try {
                const user = retrieveUser();
                setUser(user);
                setLoading(false);
            } catch (error) {
                console.error(error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    useEffect(() => {
        console.log("cvData", cvData);
        setNewCvData(
            {
                firstname: cvData.firstname,
                lastname: cvData.lastname,
                shortBio: cvData.shortBio,
                email: cvData.email,
                phoneNumber: cvData.phoneNumber,
                website: cvData.website,
                city: cvData.city,
                country: cvData.country,
                postalCode: cvData.postalCode,
                isPublic: cvData.isPublic
            }
        );
        setIsPublic(cvData.isPublic);
        handleDate(cvData.educations, newEducationData, setNewEducationData, prevEducationsRef.current);
        handleDate(cvData.experiences, newExperienceData, setNewExperienceData, prevExperiencesRef.current);
    }, [cvData]);

    useEffect(() => {
        if (user !== undefined) {
            setFullName(
                trimString(`${user.firstname} ${user.lastname}`, 16)
            );
            console.log("fullName", fullName);
        }
    }, [fullName]);

    // useEffect(() => {
    //     prevEducationsRef.current = newEducationData;
    // }, []);

    // useEffect(() => {
    //     prevExperiencesRef.current = newExperienceData;
    // }, []);





    const handleDate = (arr, newArr, setMethod, prevRef) => {
        newArr = [...arr];
        newArr.forEach((item) => {
            item.startDate = formatDate(item.startDate);
            item.endDate = formatDate(item.endDate);
        });
        setMethod(newArr);
        prevRef = arr;
    }

    const formatDate = (date) => {
        if (!date) {
            return '';
        }
        const originalDate = new Date(date);
        const formattedDate = dateFormat(originalDate, "yyyy-mm-dd");
        return formattedDate;
    }

    const handleSave = async () => {
        const deletedEdu = detectDeletion((prevEducationsRef.current || []), newEducationData || []);
        const deletedExp = detectDeletion((prevExperiencesRef.current || []), newExperienceData || []);

        try {
            await saveCvData();

            if (deletedEdu.length > 0) {
                await deletedEdu.forEach((education) => {
                    deleteEducation(education.id);
                });
                console.log("save 1")
            }
            await newEducationData.forEach((education) => {
                saveEducation(education);
                console.log("save 2")
            });

            if (deletedExp.length > 0) {
                await deletedExp.forEach((experience) => {
                    deleteExperience(experience.id);
                });
                console.log("save 3")
            }
            await newExperienceData.forEach((experience) => {
                saveExperience(experience);
                console.log("save 4")
            });

        } catch (error) {
            console.error(error);
        } finally {
            setEditMode(false);
        }
    };

    const saveCvData = async () => {
        const response = await updateCV(newCvData);
        if (response?.status === 200) {
            console.log(response);
        }
        else {
            console.error(response);
        }
    }

    const saveEducation = async (education) => {
        const response = await updateEducation(education);
        if (response?.status === 200) {
            console.log(response);
        }
        else {
            console.error(response);
        }
    }

    const saveExperience = async (experience) => {
        const response = await updateExperience(experience);
        if (response?.status === 200) {
            console.log(response);
        }
        else {
            console.error(response);
        }
    }

    const deleteExpFromList = (e) => {
        const id = e.target.id;
        const index = newExperienceData.findIndex((item) => item.id == id);
        const newData = [...newExperienceData];

        if (index !== -1) {
            newData.splice(index, 1);
            setNewExperienceData(newData);
        } else {
            console.log("not found");
        }
    }

    const deleteEduFromList = (e) => {
        const id = e.target.id;
        const index = newEducationData.findIndex((item) => item.id == id);
        const newData = [...newEducationData];

        if (index !== -1) {
            newData.splice(index, 1);
            setNewEducationData(newData);
        } else {
            console.log("not found");
        }
    }

    const detectDeletion = (prevList, currentList) => {
        const prev = prevList;
        const current = currentList;

        console.log("prev", prev);
        console.log("current", current);

        const deleted = prev.filter(prev => !current.some(current => current.id === prev.id));

        if (deleted.length > 0) {
            return deleted;
        } else {
            return [];
        }
    }

    const handleCvChange = (e) => {
        setNewCvData({
            ...newCvData,
            [e.target.name]: e.target.value,
        });
    };

    const handleEducationChange = (e) => {
        const id = e.target.id;
        const index = newEducationData.findIndex((item) => item.id == id);
        const newData = [...newEducationData];

        if (index !== -1) {
            newData[index] = {
                ...newData[index],
                [e.target.name]: e.target.value,
            };
            setNewEducationData(newData);
        }
    };

    const handleExperienceChange = (e) => {
        const id = e.target.id;
        const index = newExperienceData.findIndex((item) => item.id == id);
        const newData = [...newExperienceData];

        if (index !== -1) {
            newData[index] = {
                ...newData[index],
                [e.target.name]: e.target.value,
            };
            setNewExperienceData(newData);
        }
    };

    const handleAdd = async (e) => {
        setCreateFunction(e.target.name);
        setShowPopup(true);
    }

    const addToEduFromPopup = (newData) => {
        setCvData(prevData => {
            const updatedData = { ...prevData };
            updatedData.educations = [...updatedData.educations, newData];
            return updatedData;
        });
    }

    const addToExpFromPopup = (newData) => {
        setCvData(prevData => {
            const updatedData = { ...prevData };
            updatedData.experiences = [...updatedData.experiences, newData];
            return updatedData;
        });
    }

    const handleIsPublic = (e) => {
        setIsPublic(e.target.checked);
        setNewCvData({
            ...newCvData,
            isPublic: e.target.checked,
        });
    }











    if (loading || isLoading) {
        return <h1>Loading...</h1>;
    }

    if (!editMode) {
        window.location.reload();
    }

    return (
        <div className="profile-page-container">
            {showPopup &&
                <PopupAdd
                    onCreate={createFuction}
                    addToEduList={addToEduFromPopup}
                    addToExpList={addToExpFromPopup}
                    onClose={() => setShowPopup(false)}
                />}
            <div className="aside-panel">
                <div className='aside-panel-chid'>
                    <img src="src/assets/pp2.png" alt="User Avatar" className="profile-avatar" />
                    <h3>{fullName}</h3>
                    <div>
                        <div>
                            <label htmlFor="city">City:</label>
                            <input type="text" name='city' value={newCvData.city} onChange={handleCvChange} />
                        </div>
                        <div>
                            <label htmlFor="email">email:</label>
                            <input type="email" name='email' value={newCvData.email} onChange={handleCvChange} />
                        </div>
                        <div>
                            <label htmlFor="phoneNumber">PhoneNumber:</label>
                            <input type="text" name='phoneNumber' value={newCvData.phoneNumber} onChange={handleCvChange} />
                        </div>
                        <div>
                            <label htmlFor="website">Website:</label>
                            <input type="text" name='website' value={newCvData.website} onChange={handleCvChange} />
                        </div>
                        <div className='div-isPublic'>
                            <label htmlFor='isPublic'>Public? </label>
                            <input type="checkbox" name='isPublic' checked={isPublic} onChange={handleIsPublic} />
                        </div>
                    </div>
                </div>
                <div className='edit-btns'>
                    <button onClick={handleSave} className='edit-btn btn-save'>Save</button>
                    <button onClick={() => setEditMode(false)} className='edit-btn btn-cancel'>Cancel</button>
                </div>
            </div>
            <div className="main-content">
                <div>
                    <div className='div-title'>
                        <h3>Short bio:</h3>
                    </div>
                    <div className='div-part'>
                        <div className='div-shortBio'>
                            <textarea name='shortBio' value={newCvData.shortBio} onChange={handleCvChange} />
                        </div>
                        {cvSkillData.length > 0 &&
                            <div>
                                <h3 className='skills-title'>Skills:</h3>
                                <div className='skills'>
                                    {cvSkillData.map((skill) => (
                                        <input type="button" key={skill.id} className='btn-skill' value={skill.name} />
                                    ))}
                                </div>
                            </div>
                        }
                    </div>
                    <div className='div-title'>
                        <h3>Educations:</h3>
                    </div>
                    {newEducationData.length > 0 &&
                        <div className='div-part div-part-edit'>
                            {newEducationData.map((education) => (
                                <div key={education.id} className='div-part-child'>
                                    <div>
                                        <label htmlFor="title">Title:</label>
                                        <input type="text" id={education.id} name='title' value={education.title} onChange={handleEducationChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="startDate">Start date:</label>
                                        <input type="date" id={education.id} name='startDate' value={education.startDate} onChange={handleEducationChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="endDate">End date:</label>
                                        <input type="date" id={education.id} name='endDate' value={education.endDate} onChange={handleEducationChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="description">Description:</label>
                                        <textarea id={education.id} name='description' value={education.description} onChange={handleEducationChange} />
                                    </div>
                                    <button name='deleteEduFromList' id={education.id} onClick={deleteEduFromList} className='btn-delete'>Delete {education.title}</button>
                                </div>
                            ))}
                        </div>
                    }
                    <button name='createEducation' onClick={handleAdd} className='btn-add'>Add education</button>
                    <div className='div-title'>
                        <h3>Experiences:</h3>
                    </div>
                    {newExperienceData.length > 0 &&
                        <div className='div-part div-part-edit'>
                            {newExperienceData.map((experience) => (
                                <div key={experience.id} className='div-part-child'>
                                    <div>
                                        <label htmlFor="title">Title:</label>
                                        <input type="text" id={experience.id} name='title' value={experience.title} onChange={handleExperienceChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="startDate">Start date:</label>
                                        <input type="date" id={experience.id} name='startDate' value={experience.startDate} onChange={handleExperienceChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="endDate">End date:</label>
                                        <input type="date" id={experience.id} name='endDate' value={experience.endDate} onChange={handleExperienceChange} />
                                    </div>
                                    <div>
                                        <label htmlFor="description">Description:</label>
                                        <textarea id={experience.id} name='description' value={experience.description} onChange={handleExperienceChange} />
                                    </div>
                                    <button name='deleteExpFromList' id={experience.id} onClick={deleteExpFromList} className='btn-delete'>Delete {experience.title}</button>
                                </div>
                            ))}
                        </div>
                    }
                    <button name='createExperience' onClick={handleAdd} className='btn-add'>Add experience</button>
                </div>
            </div>
        </div>
    );
};


export default EditProfile;

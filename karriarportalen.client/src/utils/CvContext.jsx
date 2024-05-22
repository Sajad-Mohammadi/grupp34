import axios from 'axios';
import React, { createContext, useEffect, useState, useContext } from 'react';
import dateFormat from 'dateformat';
import { authService } from '../Services/authService';

const CVContext = createContext();

const useCvContext = () => {
    return useContext(CVContext);
};

const CVProvider = ({ children }) => {

    const [cvData, setCvData] = useState({
        shortBio: "",
        educations: [{ id: 0, title: "No education found", startDate: "", endDate: "", description: "" }],
        experiences: [{ id: 0, title: "No experience found", startDate: "", endDate: "", description: "" }]
    });
    const [cvSkillData, setCvSkillData] = useState(null);
    const [isLoading, setLoading] = useState(true);

    let token = authService.getToken();
    let userID = authService.getUserId();

    useEffect(() => {
        if (!authService.isLoggedIn()) {
            return;
        }
        setLoading(true);
        fetchCvData();
        fetchCvSkillData();
    }, []);

    const fetchCvData = async () => {
        try {
            const response = await axios.get(`CV/${userID}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });


            response.data.educations.map((education) => {
                education.startDate = formartDate(education.startDate);
                education.endDate = formartDate(education.endDate);
            });

            response.data.experiences.map((experience) => {
                experience.startDate = formartDate(experience.startDate);
                experience.endDate = formartDate(experience.endDate);
            });

            setCvData(prevCvData => ({
                ...prevCvData,
                firstname: response.data.firstname,
                lastname: response.data.lastname,
                shortBio: response.data.shortBio,
                email: response.data.email,
                phoneNumber: response.data.phoneNumber,
                website: response.data.website,
                city: response.data.city,
                country: response.data.country,
                postalCode: response.data.postalCode,
                experiences: response.data.experiences,
                educations: response.data.educations,
                isPublic: response.data.isPublic
            }));

        } catch (error) {
            console.error(error);
        }
    }

    const fetchCvSkillData = async () => {
        setLoading(true);
        try {
            const response = await axios.get(`CVSkill?cvId=${userID}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            setCvSkillData(response.data);
        }
        catch (error) {
            console.error(error);
        }
        finally {
            setLoading(false);
        }
    };

    const updateCV = (newCVData) => {
        if (!newCVData) {
            setCVData({});
            return;
        }
        setCVData(newCVData);
    };

    const clearCV = () => {
        setCVData({});
    };

    const retrieveCV = () => {
        return cvData;
    }

    const retrieveCVSkill = () => {
        return cvSkillData;
    }

    return (
        <CVContext.Provider value={{ fetchCvData, retrieveCV, retrieveCVSkill, updateCV, clearCV, isLoading }}>
            {children}
        </CVContext.Provider>
    );
};

function formartDate(date) {
    if (!date) {
        return "";
    }
    const originalDate = new Date(date);
    const formattedDate = dateFormat(originalDate, "mmmm yyyy");
    return formattedDate;
}

export { CVProvider, useCvContext };
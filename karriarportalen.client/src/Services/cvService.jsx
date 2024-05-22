
import React, { useEffect } from 'react';
import dateFormat from 'dateformat';
import axios from 'axios';
import { authService } from './authService';



const getAllCVs = async (search) => {
    const searchQuery = search ? `?search=${search}` : '';

    if (!authService.isLoggedIn()) {
        try {
            const response = await axios.get(`CV${searchQuery}`, {
                headers: {
                }
            });

            response.data.map((cv) => {
                cv.educations.map((education) => {
                    education.startDate = formartDate(education.startDate);
                    education.endDate = formartDate(education.endDate);

                });
                cv.experiences.map((experience) => {
                    experience.startDate = formartDate(experience.startDate);
                    experience.endDate = formartDate(experience.endDate);
                });
            });

            console.log(response);

            return response.data;
        } catch (error) {
            return error;
        }
    }
    else {
        try {
            const response = await axios.get(`CV${searchQuery}`, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });

            response.data.map((cv) => {
                cv.educations.map((education) => {
                    education.startDate = formartDate(education.startDate);
                    education.endDate = formartDate(education.endDate);

                });
                cv.experiences.map((experience) => {
                    experience.startDate = formartDate(experience.startDate);
                    experience.endDate = formartDate(experience.endDate);
                });
            });

            console.log(response.data)

            return response.data;
        } catch (error) {
            return error;
        }
    }
}

const updateCV = async (formData) => {
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.put("CV/update", formData, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        } catch (error) {
            return error;
        }
    }
}

const createEducation = async (education) => {
    console.log('education', education)
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.post("Education", education, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            console.log(response);
            return response;
        }
        catch (error) {
            return error;
        }
    }
}

const updateEducation = async (education) => {
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.put(`Education/${education.id}`, education, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        } catch (error) {
            return error;
        }
    }
}

const deleteEducation = async (id) => {
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.delete(`Education/${id}`, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        }
        catch (error) {
            return error;
        }
    }
}

const createExperience = async (experience) => {
    console.log('experience', experience)
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.post("Experience", experience, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        }
        catch (error) {
            return error;
        }
    }
}

const updateExperience = async (experience) => {
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.put(`Experience/${experience.id}`, experience, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        } catch (error) {
            return error;
        }
    }
}

const deleteExperience = async (id) => {
    if (!authService.isLoggedIn()) {
        return null;
    }
    else {
        try {
            const response = await axios.delete(`Experience/${id}`, {
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`,
                }
            });
            return response;
        }
        catch (error) {
            return error;
        }
    }
}


function formartDate(date) {
    if (!date) {
        return "";
    }
    const originalDate = new Date(date);
    const formattedDate = dateFormat(originalDate, "mmmm yyyy");
    return formattedDate;
}




export const cvService = {
    getAllCVs,
    updateCV,
    createEducation,
    updateEducation,
    deleteEducation,
    createExperience,
    updateExperience,
    deleteExperience,
};
import React, { useEffect, useState } from 'react';
import './PopupAdd.css'
import { cvService } from '../../../Services/cvService';

const PopupAdd = ({ onClose, onCreate, addToEduList, addToExpList }) => {

    const [newData, setNewData] = useState({});
    const { createExperience, createEducation } = cvService;

    const handleChange = (e) => {
        setNewData({
            ...newData,
            [e.target.name]: e.target.value,
        });
    }

    const handleCreate = async () => {
        console.log('newData', newData);
        if (onCreate === 'createExperience') {
            const response = await createExperience(newData);
            if (response.status === 201) {
                addToExpList(response.data);
            }
            console.log(response);
        } else if (onCreate === 'createEducation') {
            const response = await createEducation(newData);
            if (response.status === 201) {
                addToEduList(response.data);
            }
            console.log(response);
        }
        onClose();
    }





    return (
        <div className="popup">
            <div className='popup-outer'>
                <div className="popup-inner">
                    <h3>Add New {onCreate === 'createExperience' ? "Experience" : "Education"}</h3>
                    <form className='form-add'>
                        <div>
                            <label htmlFor="title">Title:</label>
                            <input type="text" name='title' required onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="startDate">Start date:</label>
                            <input type="date" name='startDate' required onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="endDate">End date:</label>
                            <input type="date" name='endDate' onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="description">Description:</label>
                            <textarea name='description' onClick={handleChange} />
                        </div>
                    </form>
                </div>
                <div className='edit-btns'>
                    <button onClick={handleCreate} className='edit-btn btn-save'>Add</button>
                    <button onClick={onClose} className='edit-btn btn-cancel'>Cancel</button>
                </div>
            </div>
        </div>
    );
}

export default PopupAdd;

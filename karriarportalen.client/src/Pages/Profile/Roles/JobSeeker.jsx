

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import dateFormat from 'dateformat';
import './JobSeeker.css';
import { useCvContext } from '../../../utils/CvContext';

const JobSeeker = () => {

    const { retrieveCV, retrieveCVSkill, isLoading } = useCvContext();
    const cvData = retrieveCV();
    const cvSkillData = retrieveCVSkill();


    return (
        <div>
            {isLoading ? (
                <div>Loading...</div>
            ) : (
                <div>
                    <div className='div-part'>
                        <h3>Short bio</h3>
                        <p className='shortBio'>{cvData.shortBio}</p>
                        {cvSkillData.length > 0 &&
                            <div>
                                <h4 className='skills-title'>Skills</h4>
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
                        <div className='div-part'>
                            {cvData.experiences.map((experience) => (
                                <div key={experience.id} className='div-part-child'>
                                    <h3>{experience.title}</h3>
                                    <h5>{experience.startDate}{experience.endDate}</h5>
                                    <div>
                                        <p>{experience.description}</p>
                                    </div>
                                </div>
                            ))}
                        </div>
                    }
                    {cvData.educations.length > 0 &&
                        <div className='div-part'>
                            {cvData.educations.map((education) => (
                                <div key={education.id} className='div-part-child'>
                                    <h3>{education.title}</h3>
                                    <h5>{education.startDate}{education.endDate}</h5>
                                    <div>
                                        <p>{education.description}</p>
                                    </div>
                                </div>
                            ))}
                        </div>
                    }
                </div>
            )
            }
        </div >
    );
}

export default JobSeeker;
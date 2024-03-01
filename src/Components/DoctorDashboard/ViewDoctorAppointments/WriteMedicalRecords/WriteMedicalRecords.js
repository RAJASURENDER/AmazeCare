// JavaScript source code
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams,Link } from 'react-router-dom';
// import '../WriteMedicalRecords/WriteMedicalRecords.css'

const GenerateMedicalRecords = () => {
    const [medicalRecord, setMedicalRecord] = useState({

        currentSymptoms: '',
        physicalExamination: '',
        treatmentPlan: '',
        recommendedTests: '',
        appointmentId: ''
    });

    const { appointmentId } = useParams();

    useEffect(() => {
        setMedicalRecord(prevState => ({
            ...prevState,
            appointmentId: appointmentId
        }));
    }, [appointmentId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setMedicalRecord(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post('http://localhost:5244/api/MedicalRecord/AddMedicalRecord', medicalRecord);
            alert('Medical record generated successfully!');
            // You can redirect or perform any other action after successful submission
        } catch (error) {
            console.error('Error generating medical record:', error);
            alert('The Medical Record for this Appointment is Already Generated !!!!!!!!!!');
        }
    };

    return (
        <div className="generate-medical-record">
            <nav className="generate-medical-record-navbar navbar-expand-lg ">
                <a className="generate-medical-record-navbar-brand" href="/">
                    <img src="images/logo-no-background.png" className="d-inline-block align-top" alt="" />
                </a>
                <Link className="btn btn-danger float-right" to="/login">Logout</Link>
            </nav>
            <div className='generate-medical-records-container'>
                <div className='generate-medical-records-box'>
                    <h2>Generate Medical Record</h2>
                    <form onSubmit={handleSubmit}>

                        <div className="form-group">
                            <label htmlFor="appointmentId">Appointment ID</label>
                            <input type="text" className="form-control" id="appointmentId" name="appointmentId" value={medicalRecord.appointmentId} readOnly />
                        </div>
                        <div className="form-group">
                            <label htmlFor="currentSymptoms">Current Symptoms</label>
                            <input type="text" className="form-control" id="currentSymptoms" name="currentSymptoms" value={medicalRecord.currentSymptoms} onChange={handleChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="physicalExamination">Physical Examination</label>
                            <input type="text" className="form-control" id="physicalExamination" name="physicalExamination" value={medicalRecord.physicalExamination} onChange={handleChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="treatmentPlan">Treatment Plan</label>
                            <input type="text" className="form-control" id="treatmentPlan" name="treatmentPlan" value={medicalRecord.treatmentPlan} onChange={handleChange} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="recommendedTests">Recommended Tests</label>
                            <input type="text" className="form-control" id="recommendedTests" name="recommendedTests" value={medicalRecord.recommendedTests} onChange={handleChange} />
                        </div>
                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default GenerateMedicalRecords;
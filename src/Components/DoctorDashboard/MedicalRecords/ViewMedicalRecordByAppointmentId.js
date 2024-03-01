// JavaScript source code

import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
// import '../../AdminDashboard/Patient/Patient.css';

const MedicalRecordByAppointment = () => {
    const [medicalRecords, setMedicalRecords] = useState([]);
    const [selectedAppointmentId, setSelectedAppointmentId] = useState('');

    useEffect(() => {
        fetchAllMedicalRecords();
    }, []);

    const fetchAllMedicalRecords = async () => {
        try {
            const response = await axios.get("http://localhost:5244/ViewAllMedicalRecords");
            const recordsData = response.data;

            // Fetch additional details for each medical record
            const recordsWithDetails = await Promise.all(
                recordsData.map(async (record) => {
                    // Fetch appointment details
                    const appointmentResponse = await axios.get(`http://localhost:5244/ViewAppointmentByAppointmentId?id=${record.appointmentId}`);
                    const appointmentData = appointmentResponse.data;

                    // Fetch patient details
                    const patientResponse = await axios.get(`http://localhost:5244/ViewPatientById?id=${appointmentData.patientId}`);
                    const patientData = patientResponse.data;

                    // Fetch doctor details
                    const doctorResponse = await axios.get(`http://localhost:5244/ViewDoctorById?id=${appointmentData.doctorId}`);
                    const doctorData = doctorResponse.data;

                    return {
                        ...record,
                        patientName: patientData.patientName,
                        doctorName: doctorData.doctorName,
                    };
                })
            );

            setMedicalRecords(recordsWithDetails);
        } catch (error) {
            console.error('Error fetching medical records:', error);
        }
    };

    const fetchMedicalRecordsByAppointmentId = async () => {
        try {
            const response = await axios.get(`http://localhost:5244/ViewMedicalRecordByAppointmentId?Id=${selectedAppointmentId}`);
            setMedicalRecords(response.data);
        } catch (error) {
            console.error('Error fetching medical records:', error);
        }
    };

    const handleFetchMedicalRecords = () => {
        if (selectedAppointmentId.trim() !== '') {
            fetchMedicalRecordsByAppointmentId(); // Call fetchMedicalRecordsByAppointmentId if an ID is entered
        } else {
            fetchAllMedicalRecords(); // Otherwise, fetch all medical records
        }
    };

    return (
        <div className="patient-page">
            <nav className="patient-navbar navbar-expand-lg ">
                <a className="patient-navbar-brand" href="/">
                    <img src="images/logo-no-background.png" className="d-inline-block align-top" alt="" />
                </a>
                <Link className="btn btn-danger float-right" to="/login">Logout</Link>
            </nav>
            <div className="patient-container">
                <div className="appointments-box">
                    <h2 className="text-center">Medical Records</h2>
                    <div className="fetch-medical-records">
                        <input
                            type="number"
                            value={selectedAppointmentId}
                            onChange={(e) => setSelectedAppointmentId(e.target.value)}
                            placeholder="Enter Appointment ID"
                        />

                        <button className="btn btn-primary getrecords" onClick={handleFetchMedicalRecords}>Get Records</button>
                    </div>
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">Record ID</th>
                                <th scope="col">Patient Name</th>
                                <th scope="col">Doctor Name</th>
                                <th scope="col">Current Symptoms</th>
                                <th scope="col">Physical Examination</th>
                                <th scope="col">Treatment Plan</th>
                                <th scope="col">Recommended Tests</th>
                                <th scope="col">Appointment ID</th>
                                <th scope="col">Prescription</th>
                            </tr>
                        </thead>
                        <tbody>
                            {medicalRecords.map(record => (
                                <tr key={record.recordId}>
                                    <td>{record.recordId}</td>
                                    <td>{record.patientName}</td>
                                    <td>{record.doctorName}</td>
                                    <td>{record.currentSymptoms}</td>
                                    <td>{record.physicalExamination}</td>
                                    <td>{record.treatmentPlan}</td>
                                    <td>{record.recommendedTests}</td>
                                    <td>{record.appointmentId}</td>
                                    <td>
                                        {/* Update and Delete buttons */}
                                        <div className="button-container">
                                            <Link to={`/create-prescription/${record.recordId}`} className="btn btn-info">Create</Link>
                                            <Link to={`/update-prescription/${record.recordId}`} className="btn btn-warning">Update</Link>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default MedicalRecordByAppointment;
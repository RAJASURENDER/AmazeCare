// JavaScript source code
import React from 'react';
// import './DoctorDashboard.css';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';


const DoctorDashboard = () => {

    const { doctorId } = useParams();

    useEffect(() => {
        ;
    }, [doctorId]);

    console.log('Doctor ID:', doctorId);

    const handleLogout = () =>{
        if(window.confirm('Are you sure you want to logout?')) {
            window.location.href( '/' ); 
        };
    };
    return (
        <div className="DoctorDashboard">

            <nav className="navbarr">
                <a className="navbar-brand" href="/patient-dashboard">
                    <img src="../images/logo-no-background.png" className="img-fluid" alt="Logo" width="200" height="200" />
                </a>
                <Link  onClick={handleLogout}><i className="fas fa-sign-out-alt"></i><strong> Logout </strong></Link>
            </nav>

                {/* Patient Section */}
                <div className="doctor-section">
                    <div className="doctors-container">
                        <div className="doctor-box">
                            <h2 className="heading">Welcome To AmazeCare</h2>
                        <div className="text-center">
                           
                            <Link to={`/doctorappointment/${doctorId}`} className="btn btn-info x" id="viewAppointmentButton1">
                                View Appointments
                            </Link>

                            <Link to="/medicalhistory" className="btn btn-success x" id="medicalHistoryButton1">
                                Medical History
                            </Link>
                            </div>
                        </div>
                    </div>
                </div>

        </div>
    );
}

export default DoctorDashboard;

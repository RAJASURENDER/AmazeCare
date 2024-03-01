import { useState } from "react";
import axios from 'axios'; 
import { Link } from "react-router-dom";


function AddPatient() {
    
    const [username, setUsername] = useState(""); 
    const [password, setPassword] = useState("");
    const [patientName, setPatientName] = useState("");
    const [age, setAge] = useState("");
    const [gender, setGender] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState(new Date());
    const [contactNumber, setContactNumber] = useState("");

    // State variables for form input errors
    const [usernameError, setUsernameError] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [patientNameError, setPatientNameError] = useState("");
    const [ageError, setAgeError] = useState("");
    const [genderError, setGenderError] = useState("");
    const [dateOfBirthError, setDateOfBirthError] = useState("");
    const [contactNumberError, setContactNumberError] = useState("");

    // Function to validate username input
    const validateUsername = () => {
        setUsernameError(username.length < 3 || username.length > 20 ? "Username must be between 3 and 20 characters long." : "");
    }

    // Function to validate password input
    const validatePassword = () => {
        setPasswordError(password.length < 6 || password.length > 20 ? "Password must be between 6 and 20 characters long." : "");
    }

    // Function to validate patient name input
    const validatePatientName = () => {
        setPatientNameError(patientName.length < 3 || patientName.length > 50 ? "Name must be between 3 and 50 characters long." : "");
    }

    // Function to validate age input
    const validateAge = () => {
        setAgeError(isNaN(age) || age < 1 || age > 120 ? "Age must be a number between 1 and 120." : "");
    }

    // Function to validate gender input
    const validateGender = () => {
        setGenderError(gender === "" ? "Please select a gender." : "");
    }

    // Function to validate date of birth input
    const validateDateOfBirth = () => {
        setDateOfBirthError(dateOfBirth instanceof Date && dateOfBirth !== new Date() ? "Please select a date of birth." : "");
    }

    // Function to validate contact number input
    const validateContactNumber = () => {
        setContactNumberError(contactNumber.length !== 10 ? "Contact number must be 10 digits long." : "");
    }

    // Function to handle user registration
    const AddPatient = async () => {
        if(window.confirm('Are you sure you want to add the Patient?')){
            validateUsername();
            validatePassword();
            validatePatientName();
            validateAge();
            validateGender();
            validateDateOfBirth();
            validateContactNumber();


            if (usernameError || passwordError || patientNameError || ageError || genderError || dateOfBirthError || contactNumberError) {
                return;
            }

            const patient = {
                username: username,
                password: password,
                role: "Patient", 
                patientName: patientName,
                age: age,
                gender: gender,
                dateOfBirth: dateOfBirth,
                contactNumber: contactNumber
            };

            console.log(patient);

            try {
                const response = await axios.post("http://localhost:5244/RegisterPatient", patient);

                console.log(response);
                alert("Patient Added Successfully!"); 
                window.location.href = "/toPatientInfoAdmin"; 
            } catch (error) {
                console.log(error);
                alert("Adding failed. Please try again."); 
            }
        }
    };

    const handleLogout = () => {
        if(window.confirm('Are you sure you want to Logout')){
            window.location.href = "/";
        }
    }


    return (
        <div className="Update-Doctor">
            <nav className="navbarr">
                <a className="navbar-brand" href="/toPatientInfoAdmin">
                    <img src="images/logo-no-background.png" className="img-fluid" alt="" width="200" height="200" />
                </a>
                <Link onClick={handleLogout}><i className="fas fa-sign-out-alt"></i><strong> Logout </strong></Link>
            </nav>

            <div className='Update-Container'>
                <div className="divUpdate ">
                    <h1 className="update-h1"><strong>Add Patient</strong></h1>


                        {/* Username input field */}
                        {/* onBlur={validateUsername} This is used  to call the validate function when the field loses focus */}
                        {/* onChange={(e) => setUsername(e.target.value)} It is  used to update the state of username when a change occurs in the input field. The value entered by User */}

                        <div className="form-group">
                            <label><i class="fa-solid fa-hospital-user"></i> Username</label>
                            <input className="form-control" type="text" value={username} onChange={(e) => setUsername(e.target.value)} onBlur={validateUsername} />
                            {usernameError && <span className="text-danger">{usernameError}</span>}

                        </div>
                        {/* Password input field */}
                        <div className="form-group">
                            <label><i className="fa fa-unlock"></i> Password</label>
                            <input className="form-control" type="password" value={password} onChange={(e) => setPassword(e.target.value)} onBlur={validatePassword} />
                            {passwordError && <span className="text-danger">{passwordError}</span>}
                        </div>
                        {/* Patient name input field */}
                        <div className="form-group">
                            <label><i className="fa fa-user"></i> Name</label>
                            <input className="form-control" type="text" value={patientName} onChange={(e) => setPatientName(e.target.value)} onBlur={validatePatientName} />
                            {patientNameError && <span className="text-danger">{patientNameError}</span>}
                        </div>
                        {/* Age input field */}
                        <div className="form-group">
                            <label><i class="fa fa-child" aria-hidden="true"></i> Age</label>
                            <input className="form-control" type="text" value={age} onChange={(e) => setAge(e.target.value)} onBlur={validateAge} />
                            {ageError && <span className="text-danger">{ageError}</span>}
                        </div>
                        {/* Gender input field */}
                        <div className="form-group">
                            <label><i class="fa-solid fa-venus-mars"></i> Gender</label>
                            <select className="form-control" value={gender} onChange={(e) => setGender(e.target.value)} onBlur={validateGender}>
                                <option value="">--select Gender--</option>
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                                <option value="Others">Others</option>
                            </select>
                            {genderError && <span className="text-danger">{genderError}</span>}
                        </div>
                        {/* Date of birth input field */}
                        <div className="form-group">
                            <label><i class="fa-regular fa-calendar-days"></i> Date of Birth</label>
                            <input className="form-control" type="date" value={dateOfBirth} onChange={(e) => setDateOfBirth(e.target.value)} onBlur={validateDateOfBirth} />
                            {dateOfBirthError && <span className="text-danger">{dateOfBirthError}</span>}
                        </div>
                        {/* Contact number input field */}
                        <div className="form-group">
                            <label><i class="fa fa-phone"></i> Phone</label>
                            <input className="form-control" type="text" value={contactNumber} onChange={(e) => setContactNumber(e.target.value)} onBlur={validateContactNumber} />
                            {contactNumberError && <span className="text-danger">{contactNumberError}</span>}
                        </div>
                        {/* Register button */}

                        <button type="submit" className="register-button" onClick={AddPatient}>Add Patient</button>
                    </div>
                </div>
        </div>
    );
}

export default AddPatient;
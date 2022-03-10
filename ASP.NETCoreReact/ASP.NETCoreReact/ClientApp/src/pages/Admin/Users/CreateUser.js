import React , { useState , useEffect } from "react";
import { toast }                        from "react-toastify";
import { createUser }                   from "../../../functions/user";
import Select                           from 'react-select';
import Password                         from "../../../components/Form/Password";

const initialState = {
    username: "" ,
    password: "" ,
    confirmPassword: "" ,
    firstName: "" ,
    lastName: "" ,
    position: "" ,
    phoneNumber: "" ,
    persionalId: "" ,
    roleName: ""
}

const position = ["Staff" , "Student"]

const role = ["Staff" , "Student"]


const CreateUser = ( { history } ) => {
    const [values , setValues] = useState(initialState);
    const [errors , setErrors] = useState({
        username: "" ,
        password: "" ,
        confirmPassword: "" ,
        firstName: "" ,
        lastName: "" ,
        position: "" ,
        phoneNumber: "" ,
        persionalId: "" ,
        roleName: ""
    });

    useEffect(() => {
        console.log(values)
    } , [values]);

    const validateEmail = (email) => {
        return email.match(
            /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        );
    };
    
    const validatePassword = (password) => {
        return password.match(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/
        );
    };
    
    const validateForm = () => {
        let temp = {};
        temp.username = values.username !== "" ? "" : "Username is required";
        temp.username = validateEmail(values.username) ? "" : "Username need to be an email";
        temp.firstName = values.firstName !== "" ? "" : "FirstName is required";
        temp.password = values.password !== "" ? "" : "Password is required";
        temp.confirmPassword = values.confirmPassword !== "" ? "" : "Confirm Password is required";
        if( values.password !== ""){
            temp.password = validatePassword(values.password) ? "" : "Password is not strong enough";
        }
        if( values.confirmPassword !== ""){
            temp.confirmPassword = values.confirmPassword === values.password ? "" : "Confirm Password not match with password";
        }
        temp.lastName = values.lastName !== "" ? "" : "LastName is required";
        temp.position = values.position !== "" ? "" : "Position is required";
        temp.phoneNumber = values.phoneNumber !== "" ? "" : "PhoneNumber is required";
        temp.persionalId = values.persionalId !== "" ? "" : "PersionalId is required";
        temp.roleName = values.roleName !== "" ? "" : "RoleName is required";
        setErrors({ ...temp });
        return Object.values(temp).every(x => x === "");
    }

    const handleSubmit = ( e ) => {
        e.preventDefault();
        if (validateForm()) {
            createUser(values)
                .then(( res ) => {
                    console.log(res);
                    toast.success("Item is created");
                    history.push("/admin/users")
                })
                .catch(( err ) => {
                    console.log(err);
                    toast.error(err.response.data.err);
                });
        }

    };

    const handleChange = ( e ) => {
        setValues({ ...values , [e.target.name]: e.target.value });
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-light">
            <div className="bg-white p-16 rounded shadow-2xl w-2/3">

                <h2 className="text-3xl font-bold mb-10 text-gray-800">Create Users</h2>

                <form className="space-y-5" onSubmit={handleSubmit}>
                    <div>
                        <label className="block mb-1 font-bold text-gray-500">UserName</label>
                        <input type="text" onChange={handleChange} name="username" value={values.username}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {errors.username.length > 0 && <span className="text-red-500">{errors.username}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">First Name</label>
                        <input type="text" onChange={handleChange} name="firstName" value={values.firstName}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {errors.firstName.length > 0 && <span className="text-red-500">{errors.firstName}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Last Name</label>
                        <input type="text" onChange={handleChange} name="lastName" value={values.lastName}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {errors.lastName.length > 0 && <span className="text-red-500">{errors.lastName}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Phone Number</label>
                        <input type="text" onChange={handleChange} name="phoneNumber" value={values.phoneNumber}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {errors.phoneNumber.length > 0 && <span className="text-red-500">{errors.phoneNumber}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Personal Id</label>
                        <input type="text" onChange={handleChange} name="persionalId" value={values.persionalId}
                               className="w-full border-2 border-gray-200 p-3 rounded outline-none focus:border-purple-500"/>
                        {errors.persionalId.length > 0 && <span className="text-red-500">{errors.persionalId}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Position</label>
                        <Select onChange={opt => setValues({ ...values , position: opt.value })} name="position"
                                value={{ label: values.position , value: values.position }}
                                options={position.map(opt => (
                                    { label: opt , value: opt }))}
                        />
                        {errors.position.length > 0 && <span className="text-red-500">{errors.position}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Password</label>
                        <Password onChange={handleChange} name="password" value={values.password}/>
                        {errors.password.length > 0 && <span className="text-red-500">{errors.password}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Confirm Password</label>
                        <Password onChange={handleChange} name="confirmPassword" value={values.confirmPassword}/>
                        {errors.confirmPassword.length > 0 && <span className="text-red-500">{errors.confirmPassword}</span>}
                    </div>

                    <div>
                        <label className="block mb-1 font-bold text-gray-500">Role</label>
                        <Select onChange={opt => setValues({ ...values , roleName: opt.value })} name="roleName"
                                value={{ label: values.roleName , value: values.roleName }} options={role.map(opt => (
                            { label: opt , value: opt }))}
                        />
                        {errors.roleName.length > 0 && <span className="text-red-500">{errors.roleName}</span>}
                    </div>

                    <button
                        className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                    </button>
                </form>
            </div>
        </div>);
}

export default CreateUser;
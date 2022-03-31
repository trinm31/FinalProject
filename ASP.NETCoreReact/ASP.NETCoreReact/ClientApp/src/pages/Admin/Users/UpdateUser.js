import React , { useState , useEffect } from "react";
import { toast }                        from "react-toastify";
import { editUser, getUser }            from "../../../functions/user";
import Select                           from 'react-select';
import Header                           from "../../../components/nav/Header";

const initialState = {
    id: "",
    firstName: "" ,
    lastName: "" ,
    position: "" ,
    phoneNumber: "" ,
    persionalId: "" 
}

const position = ["Staff" , "Student"]

const UpdateUser = ({history, match}) => {
    const [values , setValues] = useState(initialState);
    const [errors , setErrors] = useState({
        firstName: "" ,
        lastName: "" ,
        position: "" ,
        phoneNumber: "" ,
        persionalId: "" 
    });

    // router
    const { id } = match.params;

    useEffect(() => {
        console.log(values)
    } , [values]);

    useEffect(() => {
        onLoadUser();
    } , []);
    
    const onLoadUser = () =>{
        getUser(id).then((res)=>{
            setValues(res.data);
            console.log(res.data);
        }).catch((err) => {
            console.log(err);
            toast.error(err.response.data.err);
        })
    }
    
    const validateForm = () => {
        let temp = {};
        temp.firstName = values.firstName !== "" ? "" : "FirstName is required";
        temp.lastName = values.lastName !== "" ? "" : "LastName is required";
        temp.position = values.position !== "" ? "" : "Position is required";
        temp.phoneNumber = values.phoneNumber !== "" ? "" : "PhoneNumber is required";
        temp.persionalId = values.persionalId !== "" ? "" : "PersionalId is required";
        setErrors({ ...temp });
        return Object.values(temp).every(x => x === "");
    }

    const handleSubmit = ( e ) => {
        e.preventDefault();
        if (validateForm()) {
            editUser(values)
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
        <>
            <Header/>
            <div className="min-h-screen flex items-center justify-center bg-light">
                <div className="bg-white p-16 rounded shadow-2xl w-2/3">

                    <h2 className="text-3xl font-bold mb-10 text-gray-800">Update Users</h2>

                    <form className="space-y-5" onSubmit={handleSubmit}>
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

                        <button
                            className="block w-full bg-yellow-400 hover:bg-yellow-300 p-4 rounded text-yellow-900 hover:text-yellow-800 transition duration-300">Submit
                        </button>
                    </form>
                </div>
            </div>
        </>
    );
}

export default UpdateUser;
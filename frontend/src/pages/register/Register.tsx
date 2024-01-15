import axios from "axios";
import "./register.css";
import { useState } from "react";
import { authUrl } from "../../constants/url.constants";
import { useNavigate } from "react-router-dom";



const Register = () => {

    const redirect = useNavigate();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [repassword, setRepassword] = useState("");

    const inputCheck = () => {
        if(username == "" || password == "" || repassword == ""){
            alert("Fill out given fields");
            return false;
        }

        if(password.length < 6){
            alert("Password must be more than 5 letters");
            return false;
        }

        if(password !== repassword){
            alert("Passwords do not match");
            return false;
        }
        return true;
    }

    const handleRegister = (e:any) => {
        e.preventDefault();
        if(!inputCheck()){
            return;
        }

        const newUser = {
            username,
            password
        }

        axios.post(authUrl+"/api/Auth/Register", newUser)
        .then(() => redirect("/", {state: {message : "New user has been added"}}))
        .catch((err) => console.log(err));
    }

    return (
        <div style={{boxSizing: 'content-box', display: 'flex', justifyContent:'center', width: '100%'}}>
          <form className='flex-col' autoComplete='off' style={{width: '400px'}}>
            <label>Username</label>
            <input type='text' name='animal' value={username} onChange={(e) => setUsername(e.target.value)} className='inputRegisterUser'></input>
            <label>Password</label>
            <input type='text' name='name' value={password} onChange={(e) => setPassword(e.target.value)} className='inputRegisterUser'></input>
            <label>Confirm Password</label>
            <input type='text' name='breed' value={repassword} onChange={(e) => setRepassword(e.target.value)} className='inputRegisterUser'></input>
            <div>
              <button onClick={handleRegister} className='buttonRegisterUser'>Register User</button>
            </div>
          </form>
        </div>
        
    )
    
}

export default Register
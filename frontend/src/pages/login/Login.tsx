import "./login.css";
import axios from 'axios';
import{ useState } from 'react'
import { authUrl } from '../../constants/url.constants';
import { useAuthContextValue } from '../../provider/authProvider';
import { useNavigate } from 'react-router-dom';


const Login = () => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const {setToken} = useAuthContextValue();
    const navigate = useNavigate();

    const login = async (e: any) => {
        e.preventDefault();
        
        const User = {username, password};

        try{
            const response = await axios.post(authUrl+"/api/Auth/Login", User);
            
            setToken(response.data.authorization);
            navigate("/",{replace: true});

        }
        catch(err){
            console.error(err);
        }
    }

  return (
    <div className="container">
        <form className="form" onSubmit={login}>
            <label htmlFor="username" style={{fontSize: "20px"}}>Username</label>
            <input id="username" className="loginInput" value={username} onChange={(e) => setUsername(e.target.value)}/><br />
            <label htmlFor="password" style={{fontSize: "20px"}}>Password</label>
            <input id="password" className="loginInput" type="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            <button type="submit" className="loginButton">Submit</button>
        </form>
    </div>
  )
}

export default Login
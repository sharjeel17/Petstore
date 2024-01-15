import { jwtDecode } from "jwt-decode";
import { useEffect } from "react";
import { useLocation } from "react-router-dom";
import { useAuthContextValue } from "../../provider/authProvider";

const AuthVerify = () => {
  let location = useLocation();
  const { setToken } = useAuthContextValue();

  useEffect(() => {
    let token = localStorage.getItem("token");
    if(token){
      let decodedToken = jwtDecode(token);
      console.log("Decoded Token", decodedToken);
      let currentDate = new Date();
    
      // JWT exp is in seconds
      if (decodedToken.exp! * 1000 < currentDate.getTime()) {
        console.log("Token expired.");
        setToken("");
      } else {
        console.log("Valid token");   
        
      }
    }
  }, [location]);

  return <></>;
};

export default AuthVerify;
import { Navigate, Outlet } from "react-router-dom";
import { useAuthContextValue } from "../provider/authProvider";


const ProtectedRoute = () => {
        const { token } = useAuthContextValue();
        if(!token){
            return <Navigate to="/login" />
        }

  return (
    <Outlet />
  )
    
}

export default ProtectedRoute;
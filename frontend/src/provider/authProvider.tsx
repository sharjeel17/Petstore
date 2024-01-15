import axios from "axios";
import React, {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState,
} from "react";
import { AuthContextValueType } from "../types/global.types";

const AuthContext = createContext<AuthContextValueType | undefined>(undefined);

type Props = {
    children: React.ReactNode
}

export const AuthProvider = ({children}: Props) =>{

    const [token, _setToken] = useState(localStorage.getItem("token"));

    const setToken = (newToken: string) => {
        _setToken(newToken);
    }

    useEffect(() => {
        if(token){
            axios.defaults.headers.common["Authorization"] = "Bearer" + token;
            localStorage.setItem("token", token);

        }
        else{
            delete axios.defaults.headers.common["Authorization"];
            localStorage.removeItem("token");
        }
    }, [token])

    const contextValue = useMemo(() => {
        return {token, setToken};
    }, [token])

    return (
        <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
    )
};


export const useAuthContextValue = () => {

    const value = useContext(AuthContext);

    if(value === undefined){
        throw new Error('Forgot to set context value')
    }

    return value;
};
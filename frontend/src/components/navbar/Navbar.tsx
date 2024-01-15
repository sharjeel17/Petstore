import "./navbar.scss"
import { Menu } from "@mui/icons-material";
import {NavLink, Outlet} from "react-router-dom";
import AuthVerify from "../authVerify/AuthVerify";

const Navbar: React.FC = () => {
  return (
    <>
    <div className="navbar">
        
        <div className="brand">Pet Store</div>

        <div className="hamburger">
            <Menu />
        </div>
        
        <div className="menu">
            <ul>
                <li>
                    <NavLink to="/">Home</NavLink>
                </li>
                <li>
                    <NavLink to="/products">Products</NavLink>
                </li>
                <li>
                    <NavLink to="/products/add">Add Product</NavLink>
                </li>
                <li>
                    <NavLink to="/register">Register User</NavLink>
                </li>
            </ul>
        </div>
    </div>
    <AuthVerify />
    <Outlet />
    </>
    
  )
}

export default Navbar;
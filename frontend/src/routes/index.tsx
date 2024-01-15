import { useAuthContextValue } from '../provider/authProvider'
import Login from '../pages/login/Login';
import ProtectedRoute from './ProtectedRoute';
import Home from '../pages/home/Home';
import Products from '../pages/products/Products';
import AddProduct from '../pages/addproduct/AddProduct';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Navbar from '../components/navbar/Navbar';
import Register from '../pages/register/Register';

const Routes = () => {

    const { token } = useAuthContextValue();

    const routesForNotAuthenticated = [
        {
            path:"/",
            element: <ProtectedRoute />
        },
        {
            path: "/login",
            element: <Login />
        },
        {
            path:"*",
            element: <Login />
        }
    ];

    const routesForAuthenticated = [
        {
            element: <Navbar />,
            children: [{
                path: "/",
                element: <ProtectedRoute />,
                children: [
                    {
                        path:"/",
                        element: <Home />
                    },
                    {
                        path:"/products",
                        element: <Products />,
                        children: [
                            {
                                path:"add",
                                element: <AddProduct />
                            }
                        ]
                    },
                    {
                        path:"/register",
                        element: <Register />,
                    },
                    {
                        path:"*",
                        element: <Home />
                    },
                ]
            }]
            
        }
    ]

    const router = createBrowserRouter([...(token ? routesForAuthenticated : routesForNotAuthenticated)]);

  return <RouterProvider router={router} />
}

export default Routes;
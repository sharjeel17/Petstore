import { IProduct } from "../../types/global.types";
import { useState, useEffect } from "react";
import "./products.scss";
import axios from "axios";
import { baseUrl } from "../../constants/url.constants";
import { Button } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import ImageIcon from '@mui/icons-material/Image';
import moment from "moment";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import EditModal from "../../components/editModal/EditModal";
import DeleteModal from "../../components/deleteModal/DeleteModal";
import ImageModal from "../../components/imageModal/ImageModal";


const Products:React.FC = () => {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [isOpenEdit, setIsOpenEdit] = useState<boolean>(false);
  const [isOpenDelete, setIsOpenDelete] = useState<boolean>(false);
  const [isOpenImage, setIsOpenImage] = useState<boolean>(false);
  const [selectedProduct, setSelectedProduct] = useState<IProduct>({
    id: "",
    name: "",
    animal: "",
    breed: "",
    imageSrc: "",
    imageFile: null,
    createdAt: "",
    updatedAt: ""
  });
  
  const redirect = useNavigate();
  const location = useLocation();

  
  const getProductsList = async () => {
    try {
      const response = await axios.get<IProduct[]>(baseUrl+"/Products");
      console.log(response.data);
      setProducts(response.data);
      if(location?.state){
        alert(location.state.message);
        redirect(location.pathname, {replace: true});
      }

    } catch (err) {
      alert("An error occured");
      console.error(err);
    }
  }
  
  useEffect(() => {
    getProductsList();
  }, []);

  return (
    <>
    
    <div className="products">
      <h1> Products List</h1>
      {
        products.length == 0 ? (<h1>No products</h1>) : (
          <div className="table-wrapper">
            <table>
              <thead>
                <tr>
                  <th>Animal</th>
                  <th>Name</th>
                  <th>Created</th>
                  <th>Modified</th>
                  <th>Operations</th>
                </tr>
              </thead>
              <tbody>
                {
                  products.map(product => (
                    <tr key={product.id}>
                      <td>{product.animal}</td>
                      <td>{product.name}</td>
                      <td>{moment(product.createdAt).fromNow()}</td>
                      <td>{moment(product.updatedAt).fromNow()}</td>
                      <td>
                        <Button variant="outlined" color="warning" sx={{mx:3}} onClick={() => {
                          setSelectedProduct(product); 
                          setIsOpenEdit(true);
                          setIsOpenDelete(false);
                          setIsOpenImage(false);
                          }}>
                          <Edit/>
                        </Button>
                        <Button variant="outlined" color="warning" onClick={() => {
                          setSelectedProduct(product);
                          setIsOpenDelete(true);
                          setIsOpenEdit(false);
                          setIsOpenImage(false);
                        }}>
                          <Delete/>
                        </Button>
                        <Button variant="outlined" color="warning" sx={{mx:3}} onClick={() => {
                          setSelectedProduct(product); 
                          setIsOpenImage(true);
                          setIsOpenEdit(false);
                          setIsOpenDelete(false);
                        }}>
                          <ImageIcon />
                        </Button>
                      </td>
                    </tr>
                  ))
                }
              </tbody>
            </table>
          </div>
        )
      }

      {isOpenEdit && (!isOpenDelete) && (!isOpenImage) && (
        <EditModal selectedProduct={selectedProduct} closeIcon={() => setIsOpenEdit(false)}/>
      )}

      {isOpenDelete && (!isOpenEdit) && (!isOpenImage) && (
        <DeleteModal selectedProduct={selectedProduct} closeIcon={() => setIsOpenDelete(false)}/>
      )}

      {isOpenImage && (!isOpenEdit) && (!isOpenDelete) && (
        <ImageModal selectedImage={selectedProduct.imageSrc as string} closeIcon={() => setIsOpenImage(false)}/>
      )}
    </div>
    <Outlet />
    </>
  )
}

export default Products
import React, { useState } from 'react'
import "./editmodal.css";
import { IProduct } from '../../types/global.types';
import axios from 'axios';
import { baseUrl } from '../../constants/url.constants';
import { useNavigate } from 'react-router-dom';

type Props = {
    selectedProduct: IProduct;
    closeIcon: () => void;
}

const EditModal = ({selectedProduct, closeIcon}: Props) => {
    const [updatedProduct, setUpdatedProduct] = useState<Partial<IProduct>>({animal: selectedProduct.animal,
                                                                            name: selectedProduct.name,
                                                                            breed: selectedProduct.breed,
                                                                            imageFile: null,
                                                                            imageSrc: selectedProduct.imageSrc});
    const redirect = useNavigate();

    //show selected image or else show original image
    const imagePreview = (e: React.ChangeEvent<HTMLInputElement>) => {

      if(e.target.files && e.target.files[0]){
        let imageFile: File = e.target.files[0];
        const reader = new FileReader();

        reader.onload = (x) => {
          setUpdatedProduct({
            ...updatedProduct,
            imageFile,
            imageSrc: x.target?.result
          });
        }

        reader.readAsDataURL(imageFile);
      }

      else {
        setUpdatedProduct({
          ...updatedProduct,
          imageFile: null,
          imageSrc: selectedProduct.imageSrc
        })
      }
    }
    
    //send updated product to the server/backend
    const handleUpdate = async (e:any) => {
      e.preventDefault();

      if(updatedProduct.name === '' || updatedProduct.animal === '' || updatedProduct.breed == '') {
          alert("Fill out given fields");
          return;
      }
      
      const formData = new FormData();
      formData.append('name', updatedProduct.name as string);
      formData.append('animal', updatedProduct.animal as string);
      formData.append('breed', updatedProduct.breed as string);
      formData.append('image', updatedProduct.imageFile as File);
    
      axios.put(baseUrl+`/Products/${selectedProduct.id}`, formData)
        .then(() => redirect("/products", {state: {message : "Product has been updated"}}))
        .catch((err) => console.log(err));
    }

  return (
    <div className='editModalContainer' onClick={closeIcon}>
        <div className="innerContainer" onClick={e => e.stopPropagation()}>
            <p className='closeIcon' onClick={closeIcon}>X</p>
            <h2 style={{textAlign: 'center', margin: '5px'}}>Edit product</h2>
            <div style={{height: '100%', boxSizing: 'content-box'}}>
                <form className='editModalForm'>
                    <label>Name</label>
                    <input className='inputEditModal ' type='text' value={updatedProduct.name} onChange={(e) => setUpdatedProduct({...updatedProduct, name: e.target.value})}/>
                    <label>Animal</label>
                    <input className='inputEditModal ' type='text' value={updatedProduct.animal} onChange={(e) => setUpdatedProduct({...updatedProduct, animal: e.target.value})}/>
                    <label>Breed</label>
                    <input className='inputEditModal ' type='text' value={updatedProduct.breed} onChange={(e) => setUpdatedProduct({...updatedProduct, breed: e.target.value})}/>
                    <label>Image</label>
                    <div>
                      <img src={updatedProduct.imageSrc as string} style={{maxWidth:'250px', maxHeight: '250px'}}/>
                    </div>
                    <input type='file' accept='image/png, image/jpeg' onChange={imagePreview} style={{marginTop: '4px'}} />
                    <button className='buttonSave' onClick={handleUpdate}>Save</button>
                </form>
            </div>
        </div>
    </div>
  )
}

export default EditModal;
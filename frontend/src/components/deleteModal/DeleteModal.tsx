import { IProduct } from '../../types/global.types'
import "../editModal/editmodal.css"
import "./deletemodal.css"
import axios from 'axios'
import { baseUrl } from '../../constants/url.constants'
import { useNavigate } from 'react-router-dom'

type Props = {
    selectedProduct: IProduct;
    closeIcon: () => void;
}

const DeleteModal = ({selectedProduct, closeIcon}: Props) => {
    
const redirect = useNavigate();

const handleDelete = async () => {
    axios.delete(baseUrl+`/${selectedProduct.id}`)
            .then(() => redirect(0)) //refreshes page
            .catch((err) => console.log(err));
}

  return (
    <div className='deleteModalOverlay' onClick={() => closeIcon()}>
        <div className='innerAfterOverlay flex-between'>
            <div className='deleteModal' onClick={(e) => e.stopPropagation()}>
                <h2 style={{marginTop: '20px', textAlign: 'center'}}>Are you sure you want to <span style={{fontWeight:'bold', color: 'red'}}>Delete</span>?</h2>
                <div style={{display: 'flex', justifyContent: 'center', gap: '6px'}}>
                    <button className="buttonSave" onClick={handleDelete}>Yes</button>
                    <button className='buttonSave' onClick={closeIcon}>Back</button>
                </div>
            </div>
        </div>
    </div>
  )
}

export default DeleteModal
import "./imagemodal.css";

type Props = {
    selectedImage: string;
    closeIcon: () => void;
}
const ImageModal = ({selectedImage, closeIcon}: Props) => {
  return (
    <div className="imagemodalcontainer" onClick={closeIcon}>
        <div className="image-modal-inner-container" onClick={(e) => e.stopPropagation()}>
            <div style={{height: '100%', width: '100%'}}>
                <p className='closeIcon' onClick={closeIcon}>X</p>
                <div className='imagecontainer'>
                    <img style={{maxHeight:'100%', maxWidth: '100%'}} src={selectedImage} />
                </div>
            </div>
        </div>
    </div>
  )
}

export default ImageModal
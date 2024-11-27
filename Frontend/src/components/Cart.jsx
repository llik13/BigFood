import React from 'react';
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';

class Cart extends React.Component {
    render () {
        return(
            <Popup className="" trigger={<button> Trigger</button>} position="bottom center">
                <div className='w-[200px] text-center rounded-lg'>Your cart is empty.</div>
            </Popup>
        );
    }
}

export default Cart;
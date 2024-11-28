import React from "react";
import Item from "../../item/item.jsx";
import ItemDummy from "../../item/item-dummy.json";
import "./order-info.css";

export default class  OrderInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            cash: false,
            card: true
        };
    }

    handlePaymentChange = (e) => {
        if( e.target.id === "cash" ){
            this.setState({cash: true, card: false});
        } else {
            this.setState({cash: false, card: true});
        }
    }

    routeChange = () => {
        window.location.href = "/checkout/success";
    }

    render() {
        return (
            <div className={"order-info"}>
                <Item
                    className={'checkout-page'}
                    image={ItemDummy.image}
                    name={ItemDummy.name}
                    type={ItemDummy.type}
                    price={ItemDummy.price}
                    count={1}/>
                {/*
                Example for order modal/popup
                <Item
                    className={'order-page'}
                    image={ItemDummy.image}
                    name={ItemDummy.name}
                    type={ItemDummy.type}
                    price={ItemDummy.price}
                    count={1}/>

                Example for menu page
                <Item
                    className={'menu-page'}
                    image={ItemDummy.image}
                    name={ItemDummy.name}
                    type={ItemDummy.type}
                    price={ItemDummy.price}
                    count={1}
                    description={ItemDummy.description}/>
                    */}
                <div className="order-info-data">
                    <p>Discount: 0₴</p>
                    <p>Delivery: 40₴</p>
                    <p className="bold">Total: 305₴</p>
                </div>
                <fieldset className="order-info-payment">
                    <legend className="heading">Payment method:</legend>
                    <input type="radio" id={"cash"} name={"payment"} checked={this.state.cash} onChange={this.handlePaymentChange} />
                    <label htmlFor="cash">Cash</label>
                    <input type="radio" id={"card"} name={"payment"} checked={this.state.card} onChange={this.handlePaymentChange} />
                    <label htmlFor="card">Online/Card</label>
                </fieldset>
                {this.state.card && <div className={"card-wrapper"}>
                    <div className="online-pay">
                        <i className="applePay" style={{'--img': `url('/applepay.svg')`}}/>
                        <i className="googlePay" style={{'--img': `url('/googlepay.svg')`}}/>
                    </div>
                    <input type="text" placeholder={"Card number"} name={"card"}/>
                    <div className="card-extend-group">
                        <input type="text" placeholder={"MM/YY"} name={"date"}/>
                        <input type="text" placeholder={"CVV"} name={"cvv"}/>
                    </div>
                </div>}
                <div className="promo-holder">
                    <input type="text" placeholder={"Enter promocode"}/>
                    <button>Activate</button>
                </div>
                <div className="submit-holder">
                    <p>Total: 305₴</p>
                    <button type={"submit"} onClick={this.routeChange}>Finish order</button>
                </div>
            </div>
        );
    }
}
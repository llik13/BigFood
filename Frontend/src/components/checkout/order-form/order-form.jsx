import React from "react";
import PhoneInput from 'react-phone-input-2';
import 'react-phone-input-2/lib/style.css';
import savedAddress from './saved-address.json';
import "./order-form.css";

export default class OrderForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { selectedAddress: null,
            name: "",
            phone: "",
            city: "",
            street: "",
            house: "",
            entrance: "",
            floor: "",
            apt: "",
            intercom: "",
            comment: ""};
    }

    handleAddressChange = (e) => {
        const selectedAddress = e.target.value;
        if ( selectedAddress ){
            const address = JSON.parse(selectedAddress);
            this.setState({
                selectedAddress: address,
                city: address.city || "",
                street: address.street || "",
                house: address.house || "",
                entrance: address.entrance || "",
                floor: address.floor || "",
                apt: address.apt || "",
                intercom: address.intercom || ""});
        } else {
            this.setState({ selectedAddress: null,
                city: "",
                street: "",
                house: "",
                entrance: "",
                floor: "",
                apt: "",
                intercom: "" });
        }
    }

    handleInputChange = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    };

    render() {
        const { selectedAddress } = this.state;
        return (
            <div className={"order-form"}>
                <input type="text" className={"text-input"} name={"name"} placeholder={"Your Name"} value={this.state.name} onChange={this.handleInputChange} />
                <PhoneInput
                    country={'ua'}
                    value={this.state.phone}
                    required={true}
                    name="phone"
                    className={"phone-input"}
                    onChange={phone => this.setState({phone})}
                />
                <select name="saved-address" id="saved-address" onChange={this.handleAddressChange}>
                    <option value="">Select saved address</option>
                    {
                        savedAddress.map((address, index) => (
                            <option key={index} value={JSON.stringify(address)}>{address.name}</option>
                        ))
                    }
                </select>
                <input type="text" placeholder={"City"} name={"city"} value={this.state.city} onChange={this.handleInputChange} />
                <div className="group">
                    <input type="text" placeholder={"Street"} name={"street"} value={this.state.street} onChange={this.handleInputChange} />
                    <input type="text" placeholder={"# House"} name={"house"} value={this.state.house} onChange={this.handleInputChange} />
                </div>
                <textarea name={"comment"} id="order-comment" cols="30" rows="3" placeholder={"Leave a comment"} onChange={this.handleInputChange}></textarea>
                <div className="group mass">
                    <input type="text" placeholder={"Entrance"} name={"entrance"} value={this.state.entrance} onChange={this.handleInputChange} />
                    <input type="text" placeholder={"Floor"} name={"floor"} value={this.state.floor} onChange={this.handleInputChange} />
                    <input type="text" placeholder={"Apt/Office"} name={"apt"} value={this.state.apt} onChange={this.handleInputChange} />
                    <input type="text" placeholder={"Intercom"} name={"intercom"} value={this.state.intercom} onChange={this.handleInputChange} />
                </div>
                {selectedAddress?.["green-zone"] && <div className={"green-zone"}>
                    <p className="info">This address is in the green zone. Delivery time up to 59 minutes. The minimum order amount is UAH 100</p>
                    <img src={"./images/greenZone.png"} alt="green"/>
                </div>}
            </div>
        );
    }
}
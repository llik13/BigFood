import React from "react";
import "./cabinet-page.css";
import { Accordion, AccordionItem } from '@szhsin/react-accordion';

export default class CabinetPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }
    render() {
        return (
            <div className="cabinet-wrapper">
                <Accordion className={"accordion"}>
                    <AccordionItem header="Order history" className={"accordion-item"} initialEntered>
                        <div className="order-wrapper">
                            <div className="order-item">
                                <span className="id">0123</span>
                                <span className="date">12:17 01/01/1970</span>
                                <span className="price">50</span>
                                <span className="place">Cyrodiil,Lake Rumare</span>
                                <button className="repeat-order">Reorder</button>
                            </div>
                        </div>
                    </AccordionItem>
                    <AccordionItem header="Personal info" className={"accordion-item"} initialEntered>
                        <div className="info-wrapper">
                            <input type="text" className="text-input" placeholder={"Name"}/>
                            <input type="text" className="text-input" placeholder={"Email"}/>
                            <input type="text" className="text-input" placeholder={"Phone"}/>
                        </div>
                    </AccordionItem>

                    <AccordionItem header="Delivery address" className={"accordion-item"} initialEntered>
                        <div className="delivery-wrapper">
                            <div className="delivery-item">
                                <span className="name">My third address</span>
                                <span className="address">Cyrodiil, Lake Rumare</span>
                            </div>
                            <button className={"add-address"}>Add address</button>
                        </div>
                    </AccordionItem>
                    <AccordionItem header="Messages" className={"accordion-item"}>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </AccordionItem>
                </Accordion>
            </div>
        );
    }
}
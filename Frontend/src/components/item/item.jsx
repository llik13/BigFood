import React from "react";
import PropTypes from "prop-types";
import "./item.css";

export default class Item extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            ...this.props,
            total: this.props.count * this.props.price,
            className: props.className ? props.className : "",
        };
    }

    handlePlusItem = () => {
        let count = this.state.count + 1;
        let price = this.state.price;
        this.setState({count: count, total: price * count});
    }

    handleMinusItem = () => {
        let count = this.state.count;
        let price = this.state.price;
        if( count-1 !== 0 ) {
            count -= 1;
            this.setState({count: count, total: price * count});
        }
    }

    render() {
        switch (this.state.className) {
            case "menu-page":
                return (
                    <div className={`item ${this.state.className}`}>
                        <i className="image" style={{'--img': `url(${this.state.image})`}}/>
                        <div className="price-block">
                            <p className="price">{this.state.total} ₴</p>
                            <div className="counter">
                                <button onClick={this.handleMinusItem}>-</button>
                                <span className="count">{this.state.count}</span>
                                <button onClick={this.handlePlusItem}>+</button>
                            </div>
                        </div>
                        <p className="name">{this.state.name}</p>
                        <p className="description">{this.state.description}</p>
                    </div>
                );
            case "order-page":
                return (
                    <div className={`item ${this.state.className}`}>
                        <i className="image" style={{'--img': `url(${this.state.image})`}}/>
                        <div className="name-block">
                            <p className="name">{this.state.name}</p>
                            <div className="price-block">
                                <div className="counter">
                                    <button onClick={this.handleMinusItem}>-</button>
                                    <span className="count">{this.state.count}</span>
                                    <button onClick={this.handlePlusItem}>+</button>
                                </div>
                                <p className="price">{this.state.total} ₴</p>
                            </div>
                        </div>
                        <i className="rm"/>
                    </div>
                );
            case "checkout-page":
                return (
                    <div className={`item ${this.state.className}`}>
                        <i className="image" style={{'--img': `url(${this.state.image})`}}/>
                        <p className="name">{this.state.name}</p>
                        <div className="counter">
                            <button onClick={this.handleMinusItem}>-</button>
                            <span className="count">{this.state.count}</span>
                            <button onClick={this.handlePlusItem}>+</button>
                        </div>
                        <p className="price">{this.state.total} ₴</p>
                        <i className="rm"/>
                    </div>
                );
        }
    }
}

Item.propTypes = {
    name: PropTypes.string,
    image: PropTypes.string,
    type: PropTypes.number.isRequired,
    price: PropTypes.number.isRequired,
    count: PropTypes.number.isRequired,
    description: PropTypes.string,
    className: PropTypes.string
}
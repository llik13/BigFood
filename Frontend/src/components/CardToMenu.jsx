import * as PropTypes from "prop-types";
import {Component} from "react";

class CardToMenu extends Component {
    render() {
        let {
            isMenu, description, price,
            isSwiper, children,
            wrapperClassName,
            childClassName,
            imageLink,
            text
        } = this.props;
        if (isSwiper) {
            return (
                <div className="cursor-pointer hover:scale-105 transition-all duration-300 w-full h-full">
                    <a href="/dish">
                        {children}
                    </a>
                </div>
            );
        }
        if (isMenu) {
            return (
                <div
                    className="cursor-pointer hover:scale-105 transition-all duration-300 w-full h-full border-4 border-black rounded-2xl relative item-specific">
                    <a href="/dish">
                        <div className="flex w-[120px] h-[120px]">
                            {children}
                        </div>
                        <div>
                            <p className="text-[18px] ">{price}</p>
                            <h3 className="text-[16px] ">{text}</h3>
                            <p className="text-[10px]">{description}</p>
                        </div>
                    </a>
                </div>
            )
        }
        return (
            <div
                className={`cursor-pointer hover:scale-105 transition-all duration-300 w-full h-full ${wrapperClassName}`}>
                <a href="/dish">
                    <div
                        className={`w-full h-full border-4 border-black rounded-2xl relative flex flex-column justify-between item-specific ${childClassName}`}
                    >
                        <h3 className="text-[40px]">{text}</h3>
                        <div className="h-full w-fit flex items-end justify-center">
                        {children}
                        </div>
                    </div>
                </a>
            </div>
        );
    }
}

CardToMenu.propTypes = {
    wrapperClassName: PropTypes.string,
    childClassName: PropTypes.string,
    imageLink: PropTypes.string,
    text: PropTypes.string,
    children: PropTypes.node,
    isSwiper: PropTypes.bool,
    isMenu: PropTypes.bool,
    description: PropTypes.string,
    price: PropTypes.number,
}

export default CardToMenu;
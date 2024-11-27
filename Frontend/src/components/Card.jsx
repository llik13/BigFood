import * as PropTypes from "prop-types";
import {Component} from "react";

class Card extends Component {
  render() {
    let {
      isSwiper, children,
      wrapperClassName,
      childClassName,
      imageLink,
      text} = this.props;
    if (isSwiper) {
      return (
          <div className="cursor-pointer hover:scale-105 transition-all duration-300 w-full h-full">
             <a href="/dish">
            {children}
             </a>
          </div>
      );
    }
    return (
        <div className={`cursor-pointer hover:scale-105 transition-all duration-300 w-full h-full ${wrapperClassName}`}>
           <a href="/dish">
          <div className={`w-full h-full border-4 border-black rounded-2xl relative item-specific ${childClassName}`}
               style={{'--img': `url(${imageLink})`}}>
            <h3 className="text-[40px]">{text}</h3>
          </div>
                 </a>
        </div>
    );
  }
}

Card.propTypes = {
  wrapperClassName: PropTypes.string,
  childClassName: PropTypes.string,
  imageLink: PropTypes.string,
  text: PropTypes.string,
  children: PropTypes.node,
  isSwiper: PropTypes.bool.isRequired,
}

export default Card;

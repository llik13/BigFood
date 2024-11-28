import React from "react";
import RouteKeys from "./route-keys.json";
import {useLocation} from "react-router-dom";
import PropTypes from "prop-types";
import PhoneInput from "react-phone-input-2";
import "./sign-page.css";
import OTPInput from 'react-otp-input';

function SignPageWrapper() {
    const location = useLocation();
    console.log({"wrapper":location});
    return <SignPage location={location} />;
}

class SignPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            phone: "",
            otp: "",
            keys: RouteKeys,
            stage: this.getStageFromPath(props.location.pathname)
        };
    }

    getStageFromPath(path) {
        return RouteKeys[path] || "";
    }

    componentDidUpdate(prevProps) {
        if (this.props.location.pathname !== prevProps.location.pathname) {
            const newStage = this.getStageFromPath(this.props.location.pathname);
            this.setState({ stage: newStage });
        }
    }

    render() {
        console.log({"stage":this.state.stage});
        return (
            <div className="sign-stage-wrapper">
                {this.state.stage === "sign-in" && <div className="stage one">
                    <p className="heading">Signing in</p>
                    <div className="form-wrapper">
                        <p className="key">Your phone number</p>
                        <PhoneInput
                            country={'ua'}
                            value={this.state.phone}
                            required={true}
                            name="phone"
                            className={"phone-input"}
                            onChange={phone => this.setState({phone})}
                        />
                        <button type={"submit"} onClick={()=>{window.location.href="/account/sign-in/code"}}>Send code</button>
                    </div>
                    <p className="sub">Don’t have an account? <a href="/account/sign-up">Sign up</a></p>
                </div>}
                {this.state.stage === "sign-in-code" && <div className="stage two">
                    <p className="heading">Signing in</p>
                    <div className="form-wrapper">
                        <p className="key">Your code</p>
                        <OTPInput
                            value={this.state.otp}
                            onChange={value => this.setState({ otp: value })}
                            numInputs={6}
                            isInputNum
                            renderSeparator={<span>&nbsp;&nbsp;</span>}
                            renderInput={(props) => <input {...props} />}
                            inputStyle={{
                                width: '40px',
                                height: '40px',
                                fontSize: '18px',
                                borderRadius: '4px',
                                border: '1px solid #ccc',
                                textAlign: 'center',
                                marginBottom: '20px',
                            }}
                            focusStyle={{
                                border: '1px solid #007BFF',
                                outline: 'none',
                            }}
                        />
                        <button type={"submit"} onClick={() => {
                            window.location.href = "/account/sign-in/success"
                        }}>Verify</button>
                    </div>
                </div>}
                {this.state.stage === "sign-in-success" && <div className="stage three">
                    <p className="heading">Signing in</p>
                    <p className="key">You have successfully logged in.</p>
                    <button type={"submit"} onClick={() => {
                        window.location.href = "/account/home"
                    }}>Cabinet
                    </button>
                </div>}
            </div>
        );
    }
}

SignPage.propTypes = {
    location: PropTypes.shape({
        pathname: PropTypes.string.isRequired
    }).isRequired
}

export default SignPageWrapper;
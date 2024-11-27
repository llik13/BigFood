import "../../components/checkout/order-info/order-info.css";
const SuccessCheckoutPage = () => {
    return (
        <section id={"success-checkout-page"} style={{minHeight: "calc(100vh - 300px"}}>
            <div className="success-data">
                <h2>Your order is being prepared!</h2>
                <h3>Your order number is 006478309</h3>
                <a href="/public">Home Page</a>
            </div>
        </section>
    );
}

export default SuccessCheckoutPage;
import OrderForm from "../../components/checkout/order-form/order-form.jsx";
import OrderInfo from "../../components/checkout/order-info/order-info.jsx";

const CheckoutPage = () => {
    return (
        <section id={"checkout-page"} style={{minHeight:"calc(100vh - 300px"}}>
            <div className={"grid grid-cols-2 px-10"}>
                <OrderForm />
                <OrderInfo />
            </div>
        </section>
    );
}

export default CheckoutPage;
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Navigation from "./components/header/Navigation.jsx";
import MainPage from "./pages/MainPage.jsx";
import Footer from "./components/Footer/footer.jsx";
import MenuPage from "./pages/MenuPage.jsx";
import DishInfoPage from "./pages/DishInfoPage.jsx";
import CheckoutPage from "./pages/checkout/CheckoutPage.jsx";
import SuccessCheckoutPage from "./pages/checkout/SuccessCheckoutPage.jsx";


const router = createBrowserRouter([
  { path: "/", element: <MainPage /> }, 
  { path:"/menu", element: <MenuPage /> },
  { path:"/dish", element: <DishInfoPage/>},
  { path:"/checkout", element: <CheckoutPage /> },
  { path:"/checkout/success", element: <SuccessCheckoutPage /> }
]);

function App() {
  return (
    <>
      <Navigation />
      <RouterProvider router={router} />
      <Footer />
    </>
  );
}

export default App;

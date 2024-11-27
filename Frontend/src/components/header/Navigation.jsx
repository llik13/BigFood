import "./custom-header.css";
import Cart from "../Cart";

const Navigation = () => {
  return (
    <div className="w-[100%] flex items-center justify-between main-header sticky top-0 left-0 right-0 z-50 bg-white">
      <div className="burger">
        <i></i>
        <i></i>
        <i></i>
      </div>
      <nav className="mob">
        <ul>
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/menu">Menu</a>
          </li>
          <li>
            <a href="/delivery">Delivery</a>
          </li>
          <li>
            <a href="/">Reviews</a>
          </li>
          <li>
        <Cart/>
          </li>
          <li>
            <a href="/">Account</a>
          </li>
        </ul>
      </nav>
      <nav>
        <ul className="flex items-center gap-12 dkt">
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/menu">Menu</a>
          </li>
          <li>
            <a href="/delivery">Delivery</a>
          </li>
        </ul>
      </nav>
      <div className="flex items-center gap-[20px] cursor-pointer ">
        <img className="w-[84px] h-16" src="/logo.ico" alt="logo" />
        <p className="font-bold text-xl text-[#000000]">BigFood</p>
      </div>
      <nav>
        <ul className="flex gap-12 dkt">
          <li>
            <a href="/reviews">Reviews</a>
          </li>
          <li>
        <Cart/>
          </li>
          <li>
            <a href="/account">Account</a>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default Navigation;

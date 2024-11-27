import Description from "../components/Description.jsx";
import MenuToList from "../components/MenuToList.jsx";
import CarouselComponent from "../components/carousel/Carousel.jsx";

const MenuPage = () => {
    return (
        <div className="">
            <section className=" bg-slate-100/45 pt-12">
                <CarouselComponent />
            </section>
            <section className="bg-slate-100/45 p-24">
                <h2 className="text-center text-[52px] my-10">Pizza</h2>
                <MenuToList />
            </section>
            <section className="bg-slate-100/45 p-10">
                <Description />
            </section>
        </div>
    );
};

export default MenuPage;
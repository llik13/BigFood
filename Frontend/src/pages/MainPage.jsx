import Description from "../components/Description.jsx";
import MenuList from "../components/MenuList.jsx";
import CarouselComponent from "../components/carousel/Carousel.jsx";

const MainPage = () => {
  return (
    <div className="">
      <section className=" bg-slate-100/45 pt-12">
        <CarouselComponent />
      </section>
      <section className="bg-slate-100/45 p-24">
        <h2 className="text-center text-[52px] my-10">Menu</h2>
        <MenuList />
      </section>
      <section className="bg-slate-100/45 p-10">
        <Description />
      </section>
    </div>
  );
};

export default MainPage;

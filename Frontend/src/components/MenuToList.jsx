import defaultImages from "./masonry/default-items2.json";
import Card from "./CardToMenu.jsx";

const MenuToList = () => {
  return (
    <div className="grid grid-cols-4 gap-8 max-h-[1000px] bg-slate-100/45">
        <Card  
            wrapperClassName="row-span-12 img-center"
            imageLink={defaultImages[0].src}
            price="245₴"
            text="Salami"
            description="tomato sauce, mozzarella cheese, salami"
            isSwiper={false}
            isMenu={true} > <img src={defaultImages[0].src} alt="" /> </Card>
        <Card
            wrapperClassName="row-span-12 img-center"
            imageLink={defaultImages[1].src}
            price="275₴"
            text="Hawaii"
            description="mozzarella, bechamel sauce, chicken fillet, 
            corn, pineapple, parmesan"
            isSwiper={false}
            isMenu={true} > <img src={defaultImages[1].src} alt="" /> </Card>
        <Card 
            wrapperClassName="row-span-12 img-center"
            imageLink={defaultImages[2].src}
            price="265₴"
            text="Pepperoni"
            description="sweet & sour sauce, mozzarella, 
            pepperoni, Parmesan, arugula"
            isSwiper={false}
            isMenu={true} > <img src={defaultImages[2].src} alt="" /> </Card>
        <Card
            wrapperClassName="row-span-12 img-center"
            imageLink={defaultImages[3].src}
            price="160₴"
            text="Margarita"
            description="tomato sauce, mozzarella cheese, tomatoes"
            isSwiper={false}
            isMenu={true} > <img src={defaultImages[3].src} alt="" /> </Card>
    </div>
  );
};

export default MenuToList;

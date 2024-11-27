import defaultImages from "./masonry/default-items.json";
import Card from "./Card.jsx";

const MenuList = () => {
  return (
    <div className="grid grid-cols-6 grid-rows-4 gap-4 max-h-[1000px] bg-slate-100/45">
        <Card
            wrapperClassName="col-span-2 row-span-3 img-center"
            imageLink={defaultImages[1].src}
            text="Burgers"
            isSwiper={false} />
        <Card
            wrapperClassName="col-start-3 img-right"
            imageLink={defaultImages[9].src}
            text="Desserts"
            isSwiper={false} />
        <Card
            wrapperClassName="col-start-4 img-right"
            imageLink={defaultImages[5].src}
            text="Sushi"
            isSwiper={false} />
        <Card
            wrapperClassName="col-span-2 row-span-2 col-start-5 img-center"
            imageLink={defaultImages[4].src}
            text="Pizza"
            isSwiper={false} />
        <Card
            wrapperClassName="col-span-2 col-start-3 row-start-2 img-right"
            childClassName="sticky-bottom"
            imageLink={defaultImages[6].src}
            text="Salads"
            isSwiper={false} />
        <Card
            wrapperClassName="col-span-2 col-start-1 row-start-4 img-right"
            childClassName="fit-right"
            imageLink={defaultImages[2].src}
            text="Drinks"
            isSwiper={false} />
        <Card
            wrapperClassName="row-span-2 col-start-3 row-start-3 img-center"
            imageLink={defaultImages[8].src}
            text="Kebabs"
            isSwiper={false} />
        <Card
            wrapperClassName="col-span-3 col-start-4 row-start-3 img-right"
            imageLink={defaultImages[3].src}
            text="Hot-Dogs"
            isSwiper={false} />
        <Card
            wrapperClassName="col-start-4 row-start-4 img-right"
            childClassName="enlarge"
            imageLink={defaultImages[7].src}
            text="Sauces"
            isSwiper={false} />
        <Card
            wrapperClassName="col-span-2 col-start-5 row-start-4 img-right"
            imageLink={defaultImages[0].src}
            text="Appetizers"
            isSwiper={false} />
    </div>
  );
};

export default MenuList;

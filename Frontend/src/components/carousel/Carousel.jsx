import { Swiper, SwiperSlide } from "swiper/react";
import Card from "../Card.jsx";
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import "./swiper-custom.css";
import { Navigation, Pagination } from "swiper/modules";

// except

import defaultItems from "./default-items.json";

const CarouselComponent = () => {
  return (
    <Swiper
      modules={[Navigation, Pagination]}
      navigation
      loop={true}
      pagination={{
        dynamicBullets: true,
        clickable: true,
      }}
      spaceBetween={50}
      slidesPerView={3}
      breakpoints={{
        320: {
          width: 320,
          slidesPerView: 1,
        },
        1024: {
          slidesPerView: 3,
        },
      }}
    >
      {Array.from(defaultItems).map((item) => (
        <SwiperSlide key={item.id}>
          <Card isSwiper={true}>
            <img src={item.src} alt={item.alt} />
          </Card>
        </SwiperSlide>
      ))}
    </Swiper>
  );
};

export default CarouselComponent;

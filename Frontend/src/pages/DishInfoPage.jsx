const DishInfoPage = () => {
    return (
        <div className="bg-slate-100/45 pt-12">
            <section className="">
            <img className="w-[300px] h-[300px] border-2 border-black rounded-lg ml-20" src="/images/pizza1.png" alt="" />
                <h2 className="text-center text-[52px] my-10">Salami</h2>
                <p className="text-center">tomato sauce, mozzarella cheese, salami</p>
            </section>
            <section className="">
            <h2 className="text-center text-[52px] my-10">Sauce</h2>
            </section>
            <section className="">
            <h2 className="text-center text-[52px] my-10">Tastes amazing with</h2>
            </section>
        </div>
      );
}
export default DishInfoPage;
const Footer = () => {
  return (
    <footer className="bg-black">
      <div className="w-[90%] mx-auto flex justify-end items-center gap-12 py-8">
        <div className="container left gap-3 w-[calc(100%-12rem)]">
          <p className="text-[#fff]">м. Чернівці</p>
          <p className="text-[#fff]">вул. Ольги Кобилянської 9-11</p>
          <p className="text-[#fff]">Індекс 58000</p>
          <a className="text-[#fff]" href="tel:+380990000911">
            38(099)000-09-11
          </a>
          <div className="payments flex justify-between w-[180px] ">
            <a href="http://next.privat24.ua" className="img">
              <img className="w-11" src="/mastercard.svg" alt="bank" />
            </a>
            <a href="http://next.privat24.ua" className="img">
              <img className="w-11" src="/applepay.svg" alt="bank" />
            </a>
            <a href="http://next.privat24.ua" className="img">
              <img className="w-11" src="/googlepay.svg" alt="bank" />
            </a>
          </div>
        </div>
        <div className="container right max-w-48 cursor-pointer ">
          <img className="w-44 h-36" src="/logo.ico" alt="logo" />
        </div>
      </div>
    </footer>
  );
};

export default Footer;

import './E404.scss';

export const E404 = () => {
  return (
    <section className="page_404">
      <div className="container">
        <div className="row">
          <div className="text-center four_zero_four">
            <div className="four_zero_four_bg">
              <h1 className="text-center four_zero_four_text">404</h1>
            </div>
            <div className="contant_box_404">
              <h1 className="detail_box"><b className="detail_content">Hmmm... </b> The page you are looking for not avaible!</h1>
              <a href="\" className="link_404 text-link">
                Go to Home
              </a>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default E404;

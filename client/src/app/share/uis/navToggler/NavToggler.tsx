export interface NavTogglerProps {
    logo?: string;
    logoMini?: string;
    logoAlt?: string;
    link?:string;
    onClick?: () => void;
}

export function NavToggler({ logo, logoMini, logoAlt, link, onClick }: NavTogglerProps) {
    if (logo && logoMini) {
        return (
            <div className="background-color text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
                <a className="navbar-brand brand-logo" href={link}>
                    <img src={logo} alt={logoAlt} />
                </a>
                <a className="navbar-brand brand-logo-mini" href={link}>
                    <img src={logoMini} alt={logoAlt} />
                </a>
                <button
                    className="navbar-menu align-self-center d-none d-lg-flex"
                    type="button"
                    onClick={onClick}
                >
                    <span className="typcn typcn-th-menu text-color" />
                </button>
            </div>
        );
    }
    return (
        <button
            className="navbar-menu d-lg-none align-self-center"
            type="button"
            onClick={onClick}
        >
            <span className="typcn typcn-th-menu text-color" />
        </button>
    );
}

export default NavToggler;
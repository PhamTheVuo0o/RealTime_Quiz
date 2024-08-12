import clsx from 'clsx';
import { useNavigate } from 'react-router-dom';
export interface SidebarIconProps {
    title: string;
    icon: string;
    link: string;
    isHref?: boolean;
    detail?: string;
    activeMain?: string;
    setActiveMain?: React.Dispatch<React.SetStateAction<string>>;
    setActiveItem?: React.Dispatch<React.SetStateAction<string>>;
}

export function SidebarIcon({ title, icon, link, detail, isHref,
    setActiveMain, setActiveItem }: SidebarIconProps) {
    const navigate = useNavigate();
    function SetActive() {
        if (setActiveMain) setActiveMain(title);
        if (setActiveItem) setActiveItem('');
        if(!isHref) navigate(link, { replace: true });
    }

    if (isHref) {
        if (detail) {
            return (
                <a className="nav-link text-color" href={link} onClick={() => SetActive()}>
                    <i className={clsx('typcn text-color', icon, 'menu-icon')} />
                    <span className="menu-title">
                        {title} <span className="badge badge-primary ml-3">{detail}</span>
                    </span>
                </a>
            );
        }
        else {
            return (
                <a className="nav-link text-color" href={link} onClick={() => SetActive()}>
                    <i className={clsx('typcn text-color', icon, 'menu-icon')} />
                    <span className="menu-title">
                        {title}
                    </span>
                </a>
            );
        }
    }

    if (detail) {
        return (
            <a className="nav-link text-color" href='#' onClick={() => SetActive()}>
                <i className={clsx('typcn text-color', icon, 'menu-icon')} />
                <span className="menu-title">
                    {title} <span className="badge badge-primary ml-3">{detail}</span>
                </span>
            </a>
        );
    }
    else {
        return (
            <a className="nav-link text-color" href='#' onClick={() => SetActive()}>
                <i className={clsx('typcn text-color', icon, 'menu-icon')} />
                <span className="menu-title">
                    {title}
                </span>
            </a>
        );
    }

}

export default SidebarIcon;
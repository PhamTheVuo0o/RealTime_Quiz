import React from 'react';
import clsx from 'clsx';
import { useTransition, animated } from '@react-spring/web'
export interface SidebarSubMenuItemProps {
    title: string;
    link: string;
    id: string;
}

export interface SidebarSubMenuProps {
    readonly title: string;
    readonly icon: string;
    readonly link: string;
    readonly details: SidebarSubMenuItemProps[];
    readonly activeMain?: string;
    readonly activeItem?: string;
    readonly setActiveMain?: React.Dispatch<React.SetStateAction<string>>;
    readonly setActiveItem?: React.Dispatch<React.SetStateAction<string>>;
    readonly detailExpand?: string;
    readonly setDetailExpand?: React.Dispatch<React.SetStateAction<string>>;
    readonly isShowSidebar?: boolean;
    readonly isHovered?: boolean;
    readonly setIsHovered?: React.Dispatch<React.SetStateAction<boolean>>;
}

export function SidebarSubMenu({ title, icon, link, details, activeMain, activeItem,
    setActiveMain, setActiveItem, detailExpand, setDetailExpand, isShowSidebar, isHovered, setIsHovered }: SidebarSubMenuProps) {
    const transitions = useTransition((isShowSidebar && detailExpand == title)
        || (isHovered && !isShowSidebar), {
        from: { maxHeight: 0, opacity: isShowSidebar ? 0 : 1 },
        enter: { maxHeight: 900, opacity: 1 },
        leave: { maxHeight: 0, opacity: isShowSidebar ? 0 : 1 },
        config: { duration: 800 }
    });

    function SetDetailExpand(input: boolean) {
        if (input) {
            if (setDetailExpand) setDetailExpand(title);
        }
        else {
            if (setDetailExpand) setDetailExpand('');
        }
        if (setIsHovered) setIsHovered(input);
    }

    function SetActive(item: string) {
        if (setActiveMain) setActiveMain(title);
        if (setActiveItem) setActiveItem(item);
        if (setIsHovered) setIsHovered(false);
    }
    const handleMouseEnter = () => {
        if (setIsHovered) setIsHovered(true);
    };

    const handleMouseLeave = () => {
        if (setIsHovered) setIsHovered(false);
    };
    if (details.length > 0) {
        return (
            <>
                <a
                    className="nav-link text-color"
                    aria-expanded={detailExpand == title}
                    href={link}
                    onMouseEnter={handleMouseEnter}
                    onMouseLeave={handleMouseLeave}
                    onClick={() => SetDetailExpand(!(detailExpand == title))}
                >
                    <i className={clsx('typcn', icon, 'menu-icon text-color')} />
                    <span className="menu-title">{title}</span>
                    <i className="typcn typcn-chevron-right menu-arrow text-color" />
                </a>
                {transitions(
                    (styles, item) =>
                        item && (
                            <animated.div style={styles}>
                                <div className={clsx('collapse show')}
                                    onMouseEnter={handleMouseEnter}
                                    onMouseLeave={handleMouseLeave}
                                >
                                    <ul className="nav flex-column sub-menu">
                                        {details.map(detail => (
                                            <li className="nav-item"
                                                key={detail.id}
                                            >
                                                <a
                                                    className={clsx('nav-link text-color', { 'active': (activeMain === title && activeItem == detail.title) })}
                                                    onClick={() => SetActive(detail.title)} href={detail.link}>
                                                    {detail.title}
                                                </a>
                                            </li>
                                        ))}
                                    </ul>
                                </div>
                            </animated.div>
                        )
                )}
            </>
        );
    }
    else {
        return (
            <a
                className="nav-link"
                href={link}
            >
                <i className={clsx('typcn', icon, 'menu-icon')} />
                <span className="menu-title">{title}</span>
            </a>
        );
    }

}

export default SidebarSubMenu;
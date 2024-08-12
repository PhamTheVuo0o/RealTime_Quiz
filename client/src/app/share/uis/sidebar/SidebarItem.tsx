import { useState } from 'react';
import clsx from 'clsx';
import {
    SidebarProfile, SidebarSearch, SidebarTitle, SidebarIcon, SidebarSubMenu,
    SidebarProfileProps, SidebarSearchProps, SidebarTitleProps, SidebarIconProps,
    SidebarSubMenuProps
} from '@ShareUis';

export interface SidebarItemProps {
    sidebarProfile?: SidebarProfileProps;
    sidebarSearch?: SidebarSearchProps;
    sidebarTitle?: SidebarTitleProps;
    sidebarIcon?: SidebarIconProps;
    sidebarSubMenu?: SidebarSubMenuProps;
}

export function SidebarItem({ sidebarProfile, sidebarSearch, sidebarTitle,
    sidebarIcon, sidebarSubMenu }: SidebarItemProps) {
    const [isHovered, setIsHovered] = useState(false);

    return (
        <li className={clsx('nav-item', {
            'active':
                ((sidebarSubMenu?.activeMain === sidebarSubMenu?.title && sidebarSubMenu?.title) ||
                    (sidebarIcon?.activeMain === sidebarIcon?.title && sidebarIcon?.title)
                )
        },
            { 'hover-open': isHovered }
        )} >
            {sidebarProfile ? <SidebarProfile {...sidebarProfile} /> : <></>}
            {sidebarSearch ? <SidebarSearch {...sidebarSearch} /> : <></>}
            {sidebarTitle ? <SidebarTitle {...sidebarTitle} /> : <></>}
            {sidebarIcon ? <SidebarIcon {...sidebarIcon} /> : <></>}
            {sidebarSubMenu ? <SidebarSubMenu {...sidebarSubMenu} isHovered={isHovered} setIsHovered={setIsHovered} /> : <></>}
        </li>
    );

}

export default SidebarItem;
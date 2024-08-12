import { useState, useMemo } from 'react';
import clsx from 'clsx';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import { NavIndicator,NavDropdownContent, NavIndicatorProps, NavDIWithImageProps, NavDIWithIconProps } from '@ShareUis';

export interface NavDropdownProps extends BaseProps {
    title:string;
    itemIndicator: NavIndicatorProps;
    itemWImages: NavDIWithImageProps[];
    itemWIcons: NavDIWithIconProps[];
    bodyElement?: HTMLElement;
}

export function NavDropdown(props: NavDropdownProps) {
    const { title, isShow,className, itemIndicator, itemWImages, itemWIcons, bodyElement, permissions, permission, permissionDetails, ...rest } = props;

    const [isExpanded, setIsExpanded] = useState(false);
    const maxCount = Math.max(itemWImages.length,itemWIcons.length);

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow){
        return (
            <li className={clsx('navbar-item d-flex', className)} {...rest}>
                <NavIndicator
                    {...itemIndicator}
                    count={maxCount}
                    isExpanded={isExpanded}
                    setIsExpanded={setIsExpanded}
                    bodyElement={bodyElement}
                />
                <NavDropdownContent title={title} isExpanded={isExpanded} itemWImages={itemWImages} itemWIcons={itemWIcons} />
            </li>
        );
    }
}

export default NavDropdown;
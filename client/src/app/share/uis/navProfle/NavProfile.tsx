import { useState, useMemo } from 'react';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import clsx from 'clsx';
import { NavProfileInfor, NavProfileContent, NavProfileInforProps, NavPIWithImageProps, NavPIWithIconProps } from '@ShareUis';

export interface NavProfileProps extends BaseProps {
    itemProfileInfo: NavProfileInforProps;
    itemWImages: NavPIWithImageProps[];
    itemWIcons: NavPIWithIconProps[];
}

export function NavProfile(props: NavProfileProps) {
    const { isShow, itemProfileInfo, itemWImages, itemWIcons, bodyElement, permissions, permission, permissionDetails, ...rest } = props;

    const [isExpanded, setIsExpanded] = useState(false);
    const mainClassName = clsx('navbar-item d-flex', { 'show': isExpanded });

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <li className={mainClassName} {...rest}>
                <NavProfileInfor
                    {...itemProfileInfo}
                    isExpanded={isExpanded}
                    setIsExpanded={setIsExpanded}
                    bodyElement={bodyElement}
                />
                <NavProfileContent isExpanded={isExpanded} itemWImages={itemWImages} itemWIcons={itemWIcons} />
            </li>
        );
    }
}

export default NavProfile;
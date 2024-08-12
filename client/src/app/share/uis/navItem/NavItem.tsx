import { useMemo } from "react";
import clsx from 'clsx';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import { Label } from '@ShareUis';
import { BaseData } from '@ShareConstants'
import { useNavigate } from 'react-router-dom';

export interface NavItemProps extends BaseProps {
    name: string;
    link: string;
    isActive: boolean;
    isHref?: boolean;
}

export function NavItem(props: NavItemProps) {
    const { name, link, isActive, isHref, className, isShow, permissions, permission, permissionDetails, ...rest } = props;
    const navigate = useNavigate();
    function handleClick() {
        navigate(link, { replace: true });
    }

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        if (isHref) {
            return (
                <li className={clsx("navbar-item d-none d-flex pdlr-20",className, { 'active': isActive})} {...rest}>
                    <a className='navbar-link text-color remove-decoration' aria-label={name} href={link}>
                        <Label
                            elementType='span'
                            maxLength={BaseData.MENU_MAX_LENGTH}>
                            {name}
                        </Label>
                    </a>
                </li>
            );
        }
        return (
            <li className={clsx("navbar-item d-none d-flex pdlr-20",className, { 'active': isActive})} {...rest}>
                <a className='navbar-link text-color remove-decoration' aria-label={name} href='#' onClick={handleClick}>
                    <Label
                        elementType='span'
                        maxLength={BaseData.MENU_MAX_LENGTH}>
                        {name}
                    </Label>
                </a>
            </li>
        );
    }
}

export default NavItem;
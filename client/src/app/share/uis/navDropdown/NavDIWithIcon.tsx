import clsx from 'clsx';
import { Label } from '@ShareUis';
import { BaseData } from '@ShareConstants'

export interface NavDIWithIconProps {
    id: string;
    icon: string;
    iconType: string;
    title: string;
    content: string;
    link: string;
}

export function NavDIWithIcon({ icon, iconType, title, content, link }: NavDIWithIconProps) {
    const iconClassName = clsx('text-white text-size-normal-x2 typcn', icon, 'mx-0');
    const iconTypeClassName = clsx('avatar', iconType);

    return (
        <a className="dropdown-item remove-decoration background-color" href={link}>
            <div className={iconTypeClassName}>
                <i className={iconClassName} />
            </div>
            <div className='pdl-15'>
                <Label
                    className='font-weight-normal text-color text-size-normal'
                    elementType='h6'
                    maxLength={BaseData.TITLE_MAX_LENGTH}>
                    {title}
                </Label>
                <Label
                    className='font-weight-light text-size-normal mb-0 text-color'
                    maxLength={BaseData.CONTENT_MAX_LENGTH}>
                    {content}
                </Label>
            </div>
        </a>
    );
}

export default NavDIWithIcon;
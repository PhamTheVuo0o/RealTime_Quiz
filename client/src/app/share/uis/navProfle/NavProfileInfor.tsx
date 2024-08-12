import { useState, useRef } from 'react';
import { Label, Image } from '@ShareUis';
import { BaseData } from '@ShareConstants'
import { EventUtil } from '@ShareUtils';

export interface NavProfileInforProps {
    avatar: string;
    avatarAlt?: string;
    link: string;
    userName?: string;
    isExpanded?: boolean;
    setIsExpanded?: React.Dispatch<React.SetStateAction<boolean>>;
    bodyElement?: HTMLElement;
    callback?: () => void;
}

export interface NavProfileInforLimitProps {
    avatar: string;
    avatarAlt?: string;
    link: string;
    userName?: string;
    callback?: () => void;
}

export function NavProfileInfor({ avatar, avatarAlt, link, userName, isExpanded, setIsExpanded, bodyElement, callback }: NavProfileInforProps) {
    const [isHaveClickedInside, setIsHaveClickedInside] = useState(false);
    const inputRef = useRef<HTMLAnchorElement>(null);

    EventUtil.useClickOutside(inputRef, () => {
        if (isHaveClickedInside) {
            setIsHaveClickedInside(false);
            if (setIsExpanded) setIsExpanded(false);
        }
    }, bodyElement);

    const handleInputClick = () => {
        setIsHaveClickedInside(true);
        if (callback && !isExpanded) {
            callback();
        }
        if (setIsExpanded) setIsExpanded(!isExpanded);
    };

    return (
        <a
            className="navbar-item d-flex remove-decoration font-weight-normal"
            href={link}
            onClick={handleInputClick}
            ref={inputRef}
        >
            <Image
                className='avatar'
                src={avatar}
                alt={avatarAlt}
                defaultSrc={BaseData.DEFAULT_AVATAR}
            />
            <Label
                className='text-color hide-lg pdl-5'
                elementType='span'
                maxLength={BaseData.TITLE_MAX_LENGTH}>
                {userName}
            </Label>
        </a>
    );
}

export default NavProfileInfor;
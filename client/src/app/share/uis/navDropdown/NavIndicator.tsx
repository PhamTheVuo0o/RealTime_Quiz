import { RefObject, useState, useRef } from 'react';
import clsx from 'clsx';
import { EventUtil } from '@ShareUtils';

export interface AnchorRef extends RefObject<HTMLAnchorElement> {}

export interface NavIndicatorProps {
    icon: string;
    link: string;
    count: number;
    countType: string;
    isExpanded?: boolean;
    setIsExpanded?: React.Dispatch<React.SetStateAction<boolean>>;
    bodyElement?: HTMLElement;
}

export function NavIndicator({ icon, link, count, countType, isExpanded,setIsExpanded, bodyElement}: NavIndicatorProps) {
    const iconClassName = clsx('typcn text-color', icon);
    const countTypeClassName = clsx('count text-white', countType);
    const [isHaveClickedInside, setIsHaveClickedInside] = useState(false);

    const inputRef = useRef<HTMLAnchorElement>(null);

    EventUtil.useClickOutside(inputRef,() => {
        if(isHaveClickedInside){
            setIsHaveClickedInside(false);
            if(setIsExpanded) setIsExpanded(false);
        }
    },bodyElement);

    const handleInputClick = () => {
        setIsHaveClickedInside(true);
        if(setIsExpanded) setIsExpanded(!isExpanded);
    };

    return (
        <a
            className="count-indicator"
            href={link}
            aria-expanded={isExpanded}
            onClick={handleInputClick}
            ref={inputRef}
        >
            <i className={iconClassName} />
            <span className={countTypeClassName}>{count}</span>
        </a>
    );
}

export default NavIndicator;
import clsx from 'clsx';
import { Label } from '@ShareUis';
import { BaseData } from '@ShareConstants'
import { useNavigate  } from 'react-router-dom';

export interface NavPIWithIconProps {
    id: string;
    icon: string;
    title: string;
    link: string;
    onClick?:() => void;
    isHref?:boolean;
}

export function NavPIWithIcon({ icon, title, link,isHref, onClick }: NavPIWithIconProps) {
    const iconClassName = clsx(icon, 'text-primary', 'mr-1 text-size-big');
    const navigate = useNavigate();

    function handleClick(){
        if(onClick) onClick();
        navigate(link, { replace: true });
    }
    
    if(isHref){
        return (
            <a className="dropdown-item remove-decoration background-color" href={link} onClick={onClick}>
                <i className={iconClassName} />
                <Label
                    elementType='h6'
                    className="pdl-15 font-weight-normal text-color text-size-normal"
                    maxLength={BaseData.TITLE_MAX_LENGTH}>
                    {title}
                </Label>
            </a>
        );
    }
    return (
        <a className="dropdown-item remove-decoration background-color" onClick={handleClick}>
            <i className={iconClassName} />
            <Label
                elementType='h6'
                className="pdl-15 font-weight-normal text-color text-size-normal"
                maxLength={BaseData.TITLE_MAX_LENGTH}>
                {title}
            </Label>
        </a>
    );
}

export default NavPIWithIcon;
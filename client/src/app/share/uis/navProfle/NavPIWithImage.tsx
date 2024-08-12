import { Label,Image } from '@ShareUis';
import { BaseData } from '@ShareConstants'
import clsx from 'clsx';
import { useNavigate } from 'react-router-dom';
export interface NavPIWithImageProps {
    id: string;
    image: string;
    imageAlt?: string;
    title: string;
    content: string;
    link: string;
    active?:boolean;
    isHref?: boolean;
}

export function NavPIWithImage({ image, imageAlt, title, content, link, active, isHref }: NavPIWithImageProps) {
    const mainClassName = clsx('dropdown-item remove-decoration background-color', { 'active': active });
    const navigate = useNavigate();
    function handleClick() {
        navigate(link);
    }
    if (isHref) {
    return (
        <a className={mainClassName} href={link}>
            <div className="avatar">
                <Image
                    src={image}
                    alt={imageAlt}
                    defaultSrc={BaseData.DEFAULT_AVATAR}
                    className="avatar"
                />
            </div>
            <div className="pdl-15">
                <Label
                    className='font-weight-normal text-color text-size-normal'
                    elementType='h5'
                    maxLength={BaseData.TITLE_MAX_LENGTH}>
                    {title}
                </Label>
                <Label
                    className='font-weight-light text-size-normal mb-0 text-color'
                    elementType='h6'
                    maxLength={BaseData.CONTENT_MAX_LENGTH}>
                    {content}
                </Label>
            </div>
            <div className="avatar">
                {active && (<i className='mdi mdi-check text-primary text-size-bigest pdl-15 font-weight-bold'></i>)}
            </div>
        </a>
    )}
    else{
        return (
            <a className={mainClassName} onClick={handleClick}>
                <div className="avatar">
                    <Image
                        src={image}
                        alt={imageAlt}
                        defaultSrc={BaseData.DEFAULT_AVATAR}
                        className="avatar"
                    />
                </div>
                <div className="pdl-15">
                    <Label
                        className='font-weight-normal text-color text-size-normal'
                        elementType='h5'
                        maxLength={BaseData.TITLE_MAX_LENGTH}>
                        {title}
                    </Label>
                    <Label
                        className='font-weight-light text-size-normal mb-0 text-color'
                        elementType='h6'
                        maxLength={BaseData.CONTENT_MAX_LENGTH}>
                        {content}
                    </Label>
                </div>
                <div className="avatar">
                    {active && (<i className='mdi mdi-check text-primary text-size-bigest pdl-15 font-weight-bold'></i>)}
                </div>
            </a>
        )
    }
}

export default NavPIWithImage;
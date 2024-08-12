import { Label, Image } from '@ShareUis';
import { BaseData } from '@ShareConstants'
export interface NavDIWithImageProps {
    id: string;
    image: string;
    imageAlt: string;
    title: string;
    content: string;
    link: string;
}

export function NavDIWithImage({ image, imageAlt, title, content, link }: NavDIWithImageProps) {

    return (
        <a className="dropdown-item remove-decoration background-color" href={link}>
            <div className="avatar">
                <Image
                    src={image}
                    alt={imageAlt}
                    defaultSrc={BaseData.DEFAULT_AVATAR}
                />
            </div>
            <div className="pdl-15">
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

export default NavDIWithImage;
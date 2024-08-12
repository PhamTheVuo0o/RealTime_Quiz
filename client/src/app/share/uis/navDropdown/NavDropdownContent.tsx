import { NavDIWithImage, NavDIWithImageProps, NavDIWithIcon, NavDIWithIconProps } from '@ShareUis';
import { Label } from '@ShareUis';
import { BaseData } from '@ShareConstants'

export interface NavDropdownContentProps {
    title: string;
    isExpanded: boolean;
    itemWImages: NavDIWithImageProps[];
    itemWIcons: NavDIWithIconProps[];
}

export function NavDropdownContent({ title, isExpanded, itemWImages, itemWIcons }: NavDropdownContentProps) {

    if(isExpanded) {
        return (
            <div
                className='dropdown-menu navbar-dropdown background-color'
            >
                <Label
                    className='mb-0 font-weight-normal text-color text-size-normal dropdown-header'
                    maxLength={BaseData.TITLE_MAX_LENGTH}>
                    {title}
                </Label>
                {itemWImages.map(item => (
                    <NavDIWithImage
                        key={item.id}
                        id={item.id}
                        image={item.image}
                        imageAlt={item.imageAlt}
                        title={item.title}
                        content={item.content}
                        link={item.link}
                    />
                ))}
    
                {itemWIcons.map(item => (
                    <NavDIWithIcon
                        key={item.id}
                        id={item.id}
                        icon={item.icon}
                        iconType={item.iconType}
                        title={item.title}
                        content={item.content}
                        link={item.link}
                    />
                ))}
            </div>
        );
    }
}

export default NavDropdownContent;
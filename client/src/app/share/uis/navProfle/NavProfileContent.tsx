import { NavPIWithImage, NavPIWithImageProps, NavPIWithIcon, NavPIWithIconProps } from '@ShareUis';

export interface NavProfileContentProps {
    isExpanded: boolean;
    itemWImages: NavPIWithImageProps[];
    itemWIcons: NavPIWithIconProps[];
}

export function NavProfileContent({ isExpanded, itemWImages, itemWIcons }: NavProfileContentProps) {
    if (isExpanded) {
        return (
            <div
                className='dropdown-menu navbar-dropdown background-color'
            >
                {itemWImages.map(item => (
                    <NavPIWithImage
                        key={item.id}
                        {...item}
                    />
                ))}
                <div className="custom-line"></div>
                {itemWIcons.map(item => (
                    <NavPIWithIcon
                        key={item.id}
                        {...item}
                    />
                ))}
            </div>
        );
    }
}

export default NavProfileContent;
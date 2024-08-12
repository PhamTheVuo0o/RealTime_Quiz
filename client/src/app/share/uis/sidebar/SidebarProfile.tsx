import { Label,Image } from '@ShareUis';
import { BaseData } from '@ShareConstants'
export interface SidebarProfileProps {
    avatar: string;
    avatarAlt: string;
    userName: string;
    statusMessage: string;
}

export function SidebarProfile({ avatar, avatarAlt, userName, statusMessage }: SidebarProfileProps) {
    return (
        <div className="d-flex sidebar-profile">
            <div className="sidebar-profile-image">
                <Image
                    src={avatar}
                    alt={avatarAlt}
                    defaultSrc={BaseData.DEFAULT_AVATAR}
                />
                <span className="sidebar-status-indicator" />
            </div>
            <div className="sidebar-profile-name">
                <Label
                    className='sidebar-name text-color'
                    elementType='p'
                    maxLength={BaseData.TITLE_MAX_LENGTH}>
                    {userName}
                </Label>
                <Label
                    className='sidebar-designation text-color'
                    elementType='p'
                    maxLength={BaseData.CONTENT_MAX_LENGTH}>
                    {statusMessage}
                </Label>
            </div>
        </div>
    );
}

export default SidebarProfile;
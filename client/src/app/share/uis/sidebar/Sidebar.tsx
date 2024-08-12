import { useState, useMemo } from 'react';
import clsx from 'clsx';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import { SidebarItem, SidebarLegend } from '@ShareUis';
import { DefaultData } from '@ShareConstants'
import { useI18nContext } from '@ShareCores';
import { useSelector } from 'react-redux';
import { RootState } from '@ShareStores';

export interface SidebarProps extends BaseProps {
    isShow: boolean;
}

export function Sidebar(props: SidebarProps) {
    const { isShow, className, ...rest } = props;
    const { t } = useI18nContext();
    const [searchText, setSearchText] = useState('');
    const [activeMain, setActiveMain] = useState('');
    const [activeItem, setActiveItem] = useState('');
    const [detailExpand, setDetailExpand] = useState('');

    const currentUserAvatar = useSelector((state: RootState) => state.GlobalVar.currentUserAvatar);
    const currentUserName = useSelector((state: RootState) => state.GlobalVar.currentUserName);

    function DoSearch() {
        console.log(`DoSearch: Key=${searchText}`);
    }

    return (
        <nav className={clsx('sidebar text-color background-color sidebar-offcanvas',className, { 'active': isShow })} {...rest}>
            <ul className="nav">
                <SidebarItem sidebarProfile={{
                    avatar: currentUserAvatar,
                    avatarAlt: "",
                    userName: currentUserName,
                    statusMessage: "Welcome",
                }}
                    sidebarSearch={{
                        onChangeSearchText: setSearchText,
                        doSearch: DoSearch
                    }}
                    sidebarTitle={{
                        title: t('dashMenu')
                    }}
                />
                <SidebarItem sidebarIcon={{
                    title: t('dashboard'),
                    link: '/',
                    icon: 'typcn-device-desktop',
                    detail: t('new'),
                    activeMain: activeMain,
                    setActiveItem: setActiveItem,
                    setActiveMain: setActiveMain,
                }} />
            </ul>
        </nav>
    )
}

export default Sidebar;
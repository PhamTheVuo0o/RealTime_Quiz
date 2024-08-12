import { useState, ReactNode, useEffect, useContext } from 'react';
import clsx from 'clsx';
import {
    DarkLightSwitch,
    NavItem,
    NavToggler,
    NavDropdown,
    NavProfile,
    ThemeSetting,
    Sidebar,
    NavPIWithIconProps,
    NavPIWithImageProps,
} from '@ShareUis';
import { DefaultData } from '@ShareConstants';
import { Footer } from '@ShareLayouts';
import { useI18nContext } from '@ShareCores';
import { useDispatch, useSelector } from 'react-redux';
import {
    setIsAuth,
    setCurrentUserEmail,
    setCurrentUserName,
    setPermissions,
    setSignedInAccounts,
    UserStorage,
    setCurrentUserAvatar,
    setIsLoading,
    RootState
} from '@ShareStores';
import { RouterAppEnum } from '@ShareEnums';
import { StringUntil } from '@ShareUtils';
import { ThemeContext } from '@ShareLayouts';

export interface NavMenuProps {
    children: ReactNode;
}

export function NavMenu({ children }: NavMenuProps) {
    const { t } = useI18nContext();
    const currentPath = window.location.pathname;
    const dispatch = useDispatch();

    const themeContext = useContext(ThemeContext);

    if (!themeContext) {
        throw new Error('App must be used within a ThemeProvider');
    }
    const { isLight, isSideBarExpand, toggleIsLight, toggleIsSideBarExpand } = themeContext;

    const [isRun, setIsRun] = useState(false);
    
    const currentUserId = useSelector((state: RootState) => state.GlobalVar.currentUserId);
    const [oldUserId, setOldUserId] = useState(currentUserId);
    const currentUserAvatar = useSelector((state: RootState) => state.GlobalVar.currentUserAvatar);
    const currentUserName = useSelector((state: RootState) => state.GlobalVar.currentUserName);
    const signedInAccounts = useSelector((state: RootState) => state.GlobalVar.signedInAccounts);

    const navProfileSetting: NavPIWithIconProps[] = [
        {
            id: "1",
            icon: "mdi mdi-shape-polygon-plus",
            title: "Add account",
            link: '',
            onClick: () => doAddAccount()
        },
        {
            id: "2",
            icon: "mdi mdi-power",
            title: "Logout",
            link: StringUntil.BRoute(RouterAppEnum.Login),
            onClick: () => doLogout(),
            isHref: true
        },
    ];
    async function doLogout() {
        const rlt = UserStorage.logoutCurrentUser();
        if (rlt) {
            dispatch(setIsAuth(false));
        }
    }
    async function doAddAccount() {
        const newTabUrl = `${window.location.origin}${StringUntil.BRoute(RouterAppEnum.Login)}`;
        window.open(newTabUrl, '_blank');
    }
    async function doGetSignedInAccounts() {
        dispatch(setIsLoading(false));
        const rlt = UserStorage.getUserTokenContents();
        if (rlt.UserTokenContents) {
            const _signedInAccounts: NavPIWithImageProps[] = [];
            rlt.UserTokenContents.forEach((item) => {
                const userName = `${item.firstName} ${item.lastName}`;
                const signedInAccout: NavPIWithImageProps = {
                    id: item.id,
                    image: item.avatar,
                    title: userName,
                    content: item.email,
                    active: item.selected,
                    link: `${RouterAppEnum.Base}${item.id}`,
                }
                if (item.selected) {
                    if (item.email) dispatch(setCurrentUserEmail(item.email));
                    dispatch(setCurrentUserName(userName));
                    dispatch(setCurrentUserAvatar(item.avatar));
                    if (item.isAuthenticated) dispatch(setIsAuth(item.isAuthenticated));
                    dispatch(setPermissions(item.permissions));
                }
                _signedInAccounts.push(signedInAccout);
            });
            if(_signedInAccounts && _signedInAccounts.length>=1){
                _signedInAccounts.sort((a, b) => {
                    // Coerce undefined to false
                    const activeA = a.active ?? false;
                    const activeB = b.active ?? false;
                    return Number(activeB) - Number(activeA);
                });
                dispatch(setSignedInAccounts(_signedInAccounts));
            }
            dispatch(setIsLoading(false));
        }
    }

    function UserProfileLoading() {
        if ((!isRun)||(oldUserId != currentUserId)) {
            setIsRun(true);
            setOldUserId(currentUserId);
            doGetSignedInAccounts();
        }
    }

    useEffect(() => {
        UserProfileLoading();
    }, [currentUserId])

    return (
        <div className={clsx('background-color', { 'sidebar-icon-only': !isSideBarExpand })}>
            <nav className={clsx('navbar border-bottom col-lg-12 col-12 p-0 fixed-top d-flex flex-row text-color background-color')}>
                <NavToggler
                    logo="/icons/logo.svg"
                    logoMini="/icons/logo_mini.svg"
                    logoAlt={t('logoAlt')}
                    link="\"
                    onClick={() => toggleIsSideBarExpand()}
                />
                <div className="navbar-menu-wrapper box-shadow-bottom-right d-flex align-items-center justify-content-end background-color">
                    <ul className="navbar-nav mr-lg-2">
                        <NavItem name={t('home')} className='pdlr-20 mr-15 hide-md' link={StringUntil.BRoute(RouterAppEnum.Home)} isActive={currentPath === StringUntil.BRoute(RouterAppEnum.Home)} />
                        <NavItem name={t('test')} className='pdlr-20 mr-15 hide-md' link={StringUntil.BRoute(RouterAppEnum.Test)} isActive={currentPath === StringUntil.BRoute(RouterAppEnum.Test)} />
                    </ul>
                    <ul className="navbar-nav navbar-nav-right">
                        <NavItem name={t('help')} link={StringUntil.BRoute(RouterAppEnum.Help)} isActive={currentPath === StringUntil.BRoute(RouterAppEnum.Help)} />
                        <DarkLightSwitch
                            isLight={isLight}
                            toggleIsLight={toggleIsLight}
                            className='navbar-item d-flex pdlr-20'
                        />
                        <NavProfile
                            className='pdl-20'
                            itemProfileInfo={{
                                avatar: currentUserAvatar,
                                avatarAlt: "",
                                link: '#',
                                userName: currentUserName,
                                callback: doGetSignedInAccounts,
                            }}
                            itemWImages={signedInAccounts}
                            itemWIcons={navProfileSetting}
                            bodyElement={document.body}
                            id={currentUserId}
                        />
                    </ul>
                    <NavToggler onClick={() => toggleIsSideBarExpand()}></NavToggler>
                </div>
            </nav>
            <div className="container-fluid page-body-wrapper">
                <Sidebar className='border-right' isShow={isSideBarExpand} />
                <div className="main-panel">
                    <div className="container-scroller background-color text-color content-wrapper">
                        {children}
                    </div>
                    <Footer
                        {...DefaultData.itemFooter}
                        className='background-color text-color'
                    />
                </div>
            </div>
        </div>
    )
}

export default NavMenu;
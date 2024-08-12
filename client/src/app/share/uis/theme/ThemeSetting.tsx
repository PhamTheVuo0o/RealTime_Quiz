import React, { useState, useRef, useMemo } from 'react';
import clsx from 'clsx';
import { useI18nContext } from '@ShareCores';
import { EventUtil, StringUntil } from '@ShareUtils';
import { BaseProps } from '@ShareUis';

export interface ThemeSettingProps extends BaseProps {
    link: string;
    isLight: boolean;
    onClickIsLight: React.Dispatch<React.SetStateAction<boolean>>;
    navColor: string;
    onClickNavColor: (input: string) => void;
}

export function ThemeSetting(props: ThemeSettingProps) {
    const { isShow, link, isLight, onClickIsLight, navColor, onClickNavColor, bodyElement, permissions, permission, permissionDetails, ...rest } = props;
    const { t } = useI18nContext();
    const [isHaveClickedInside, setIsHaveClickedInside] = useState(false);
    const [isExpanded, setIsExpanded] = useState(false);
    const inputRef = useRef<HTMLAnchorElement>(null);

    EventUtil.useClickOutside(inputRef, () => {
        if (isHaveClickedInside) {
            setIsHaveClickedInside(false);
            setIsExpanded(false);
        }
    }, bodyElement);

    const handleInputClick = () => {
        setIsHaveClickedInside(true);
    };

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <a
                className="theme-setting-wrapper background-color text-color"
                href={link}
                ref={inputRef}
                onClick={handleInputClick}
                {...rest}
            >
                <div id="settings-trigger" onClick={() => setIsExpanded(true)}>
                    <i className="typcn typcn-cog-outline" />
                </div>
                <div
                    id="theme-settings"
                    className={clsx('settings-panel background-color text-color', { 'open': isExpanded })}
                >
                    <i className="settings-close text-color typcn typcn-delete-outline" onClick={() => setIsExpanded(false)} />
                    <p className="settings-heading  text-color">{t('sidebarSkins')}</p>
                    <div className={clsx('sidebar-bg-options background-color', { 'selected': isLight })} onClick={() => onClickIsLight(true)}>
                        <div className="img-ss rounded-circle bg-light border mr-3" />
                        {t('light')}
                    </div>
                    <div className={clsx('sidebar-bg-options background-color text-color', { 'selected': !isLight })} onClick={() => onClickIsLight(false)}>
                        <div className="img-ss rounded-circle bg-dark border mr-3" />
                        {t('dark')}
                    </div>
                    <p className="settings-heading mt-2 text-color">{t('headerSkins')}</p>
                    <div className="color-tiles mx-0 px-4">
                        <div className={clsx('border tiles background-success', { 'selected': (navColor == 'background-success') })} onClick={() => onClickNavColor('background-success')} />
                        <div className={clsx('border tiles background-warning', { 'selected': (navColor == 'background-warning') })} onClick={() => onClickNavColor('background-warning')} />
                        <div className={clsx('border tiles background-danger', { 'selected': (navColor == 'background-danger') })} onClick={() => onClickNavColor('background-danger')} />
                        <div className={clsx('border tiles background-primary', { 'selected': (navColor == 'background-primary') })} onClick={() => onClickNavColor('background-primary')} />
                        <div className={clsx('border tiles background-info', { 'selected': (navColor == 'background-info') })} onClick={() => onClickNavColor('background-info')} />
                        <div className={clsx('border tiles background-secondary', { 'selected': (navColor == 'background-secondary') })} onClick={() => onClickNavColor('background-secondary')} />
                        <div className={clsx('border tiles background-color', { 'selected': (navColor == 'background-color') })} onClick={() => onClickNavColor('background-color')} />
                    </div>
                </div>
            </a>
        );
    }
}

export default ThemeSetting;
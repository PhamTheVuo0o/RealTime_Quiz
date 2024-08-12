import { BaseInputProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import { useMemo } from "react";
import clsx from 'clsx';
export interface FooterProps extends BaseInputProps {
    website: string;
    year: number;
    link: string;
    version: string;
}

export function Footer(props: FooterProps) {
    const { website, year, link, version, className, isShow, permissions, permission, permissionDetails, ...rest } = props;
    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <footer className={clsx('footer', className)}>
                <div className="d-sm-flex justify-content-center justify-content-sm-between">
                    <span className="text-center text-sm-left d-block d-sm-inline-block">Copyright © <a href={link} target="_blank">{website}</a> {year}</span>
                    <span className="float-none float-sm-right d-block mt-1 mt-sm-0 text-center">Version : <a href={link} target="_blank">{version}</a></span>
                </div>
            </footer>
        );
    }
}

export default Footer;
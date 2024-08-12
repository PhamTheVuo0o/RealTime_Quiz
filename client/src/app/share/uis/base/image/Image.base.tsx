import { useMemo, useState, useEffect } from "react";
import clsx from 'clsx';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import './Image.base.scss';
export interface ImageProps extends BaseProps {
    src: string
    defaultSrc?: string
    alt?: string
}
export function Image(props: ImageProps) {
    const { src, defaultSrc, alt, id, className, isShow, permissions, permission, permissionDetails, ...rest } = props;
    const [imgSrc, setImgSrc] = useState(src);

    useEffect(() => {
        setImgSrc(src)
    }, [src])

    const handleError = () => {
        if (defaultSrc) setImgSrc(defaultSrc);
    };

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <img
                id={id}
                src={imgSrc}
                alt={alt}
                onError={handleError}
                className={clsx(className)}
                {...rest} />
        )
    }
}

export default Image;

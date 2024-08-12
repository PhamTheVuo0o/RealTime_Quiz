import { useMemo } from "react";
import clsx from 'clsx';
import { BaseProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import './Label.base.scss';
export interface LabelProps extends BaseProps {
    content?: string;
    forInput?:string
    elementType?: 'label' | 'span' | 'p' | 'h1' | 'h2' | 'h3' | 'h4' | 'h5' | 'h6' | 'a' | 'button' | 'i';
}
export function Label(props: LabelProps) {
    const { elementType = 'p', id, forInput, content, className, isShow, permissions, permission, permissionDetails, children, maxLength, ...rest } = props;

    let _content = content;
    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    const Element: React.ElementType = elementType;

    if (_isShow && (content || children)) {
        if (content == null && StringUntil.IsString(children)) {
            _content = `${children}`;
            return (
                <Element id={id} htmlFor={forInput} className={clsx(className)} {...rest}>{StringUntil.ContentDisplay(_content, maxLength)}</Element>
            )
        }
        return (
            <Element id={id} htmlFor={forInput} className={clsx(className)} {...rest}>{StringUntil.ContentDisplay(_content, maxLength)}{children}</Element>
        )
    }
}

export default Label;

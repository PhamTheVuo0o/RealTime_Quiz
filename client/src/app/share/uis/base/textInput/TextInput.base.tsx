import { useMemo, useEffect, useState, useRef } from "react";
import clsx from 'clsx';
import { BaseInputProps } from '@ShareUis';
import { StringUntil, EventUtil } from '@ShareUtils';
import './TextInput.base.scss';
export interface TextInputProps extends BaseInputProps {
    inputType?: string;
}
export function TextInput(props: TextInputProps) {
    const { id, value, inputType, className, isShow, permissions, permission, permissionDetails, children, maxLength, rules, bodyElement, ...rest } = props;

    const [isError, setIsError] = useState(false);
    const [isFirstRun, setIsFirstRun] = useState(true);
    const [isHaveClickedInside, setIsHaveClickedInside] = useState(false);
    const [errorMessage, setErrorMessage] = useState('');

    const inputRef = useRef<HTMLInputElement>(null);

    EventUtil.useClickOutside(inputRef,() => {
        if(isHaveClickedInside){
            setIsHaveClickedInside(false);
            IsError();
        }
    },bodyElement);

    const handleInputClick = () => {
        setIsHaveClickedInside(true);
    };

    function IsError() {
        setIsError(false);
        setErrorMessage('')
        if (value && rules) {
            {
                if (rules.pattern && rules.pattern.value && rules.pattern.message) {
                    if (rules.pattern.value.test(value) == false) {
                        setErrorMessage(rules.pattern.message);
                        setIsError(true);
                    }
                }
            }
        } else {
            if (rules && rules.required) {
                setErrorMessage(rules.required);
                setIsError(true);
            }
        }
    }

    useEffect(() => {
        if (isFirstRun) {
            setIsFirstRun(false);
        }
        else {
            IsError();
        }
    }, [value])

    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <>
                <input
                    ref={inputRef} 
                    id={id}
                    type={inputType}
                    className={clsx(className,{'input-error':isError})}
                    maxLength={maxLength}
                    onClick={handleInputClick}
                    {...rest}
                >{children}</input>
                {isError && (
                    <div className='mt-5 text-primary text-small'>
                        <i className="mdi mdi-alert-circle-outline mr-5" />
                        <span>{errorMessage}</span>
                    </div>)}
            </>
        )
    }
}

export default TextInput;

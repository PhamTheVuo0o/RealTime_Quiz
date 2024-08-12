import { useMemo} from "react";
import clsx from 'clsx';
import { BaseInputProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import './DarkLightSwitch.base.scss';
export interface DarkLightSwitchProps extends BaseInputProps {
    isLight: boolean;
    toggleIsLight: () => void;
}
export function DarkLightSwitch(props: DarkLightSwitchProps) {
    const { id, toggleIsLight, isLight, className, isShow, permissions, permission, permissionDetails, ...rest } = props;


    const _isShow = useMemo(() => {
        if (isShow == null) {
            return StringUntil.IsShow(permissions, permission, permissionDetails);
        }
        return isShow;
    }, [isShow, permissions, permission, permissionDetails]);

    if (_isShow) {
        return (
            <>
                {isLight && (
                    <i
                        className={clsx('mdi mdi-weather-night text-color text-size-bigest', className)}
                        id={id}
                        onClick={toggleIsLight} {...rest} />)}
                {!isLight && (
                    <i
                        className={clsx('mdi mdi-weather-sunny text-color text-size-bigest', className)}
                        id={id}
                        onClick={toggleIsLight} {...rest} />)}

            </>
        )
    }
}

export default DarkLightSwitch;

import clsx from 'clsx';
import './CheckBox.base.scss';
import { BaseInputProps } from '@ShareUis';
import { StringUntil } from '@ShareUtils';
import { useMemo } from "react";

export interface CheckBoxProps
  extends BaseInputProps {
  label?: string;
  checked?: boolean;
}

export function CheckBox(props: CheckBoxProps) {
  const { label, id, checked, onChange, className, isShow, permissions, permission, permissionDetails, children, maxLength, ...rest } = props;
  let _label = label;
  
  const _isShow = useMemo(() => {
    if (isShow == null) {
      return StringUntil.IsShow(permissions, permission, permissionDetails);
    }
    return isShow;
  }, [isShow, permissions, permission, permissionDetails]);

  if (_isShow) {
    if (label == null && StringUntil.IsString(children)) {
      _label = `${children}`;
      return (
        <div className={clsx('checkbox-container', className)}>
          <input
            type="checkbox"
            id={id}
            checked={checked}
            onChange={onChange}
            className="mr-2"
          />
          <label className="mb-0" htmlFor={id}>{StringUntil.ContentDisplay(_label, maxLength)}</label>
        </div>
      );
    }
    return (
      <div className={clsx('checkbox-container', className)}>
        <input
          type="checkbox"
          id={id}
          checked={checked}
          onChange={onChange}
          className="text-primary mr-2"
        />
        <label className="mb-0" htmlFor={id}>{StringUntil.ContentDisplay(_label, maxLength)}</label>
        {children}
      </div>
    );
  }
}

export default CheckBox;

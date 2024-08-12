import { PermissionEnum, PermissionDetailEnum } from '@ShareEnums'
export interface BaseProps {
    id?:string;
    isShow?:boolean;
    maxLength?:number;
    permission?:PermissionEnum;
    permissionDetails?:PermissionDetailEnum[];
    permissions?:string[];
    className?: string;
    children?: React.ReactNode;
    [key: string]: any;
}

export interface patternProps {
    value?: RegExp;
    message?:string;
}

export interface ruleProps {
    required?:string;
    pattern?:patternProps
}

export interface BaseInputProps extends React.InputHTMLAttributes<HTMLInputElement | HTMLTextAreaElement> {
    id?:string;
    isShow?:boolean;
    value?:string;
    rules?:ruleProps;
    maxLength?:number;
    permission?:PermissionEnum;
    permissionDetails?:PermissionDetailEnum[];
    permissions?:string[];
    className?: string;
    children?: React.ReactNode;
    onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
    bodyElement?: HTMLElement;
    [key: string]: any;
}
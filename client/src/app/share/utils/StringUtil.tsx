import React from 'react';
import { PermissionEnum, PermissionDetailEnum, RouterAppEnum } from '@ShareEnums'
export const StringUntil = {
    IsShow: (permissions?: string[], permission?: PermissionEnum, permissionDetails?: PermissionDetailEnum[]) => {
        if (permission == null || permissionDetails == null) { return true; }

        if (permissions) {
            if (permissions.includes(`${PermissionEnum.All}:${PermissionDetailEnum.All}`) == true) { return true; }
            if (permissions.includes(`${permission}:${PermissionDetailEnum.All}`) == true) { return true; }

            return permissionDetails.some(item => (
                permissions.includes(`${permission}:${item}`) ||
                permissions.includes(`${PermissionEnum.All}:${item}`)
            ));
        }
        return false;
    },
    ContentDisplay: (content?: string, maxLength?: number) => {
        if (content && maxLength && maxLength > 3) {
            if (content.length > maxLength) {
                return `${content.substr(0, maxLength - 3)}...`;
            }
        }
        return content;
    },
    IsString: (node?: React.ReactNode) => {
        if (node) {
            return typeof node === 'string';
        }
        return false;
    },
    BRoute: (input?: RouterAppEnum) => {
        if (input != null) {
            return `${RouterAppEnum.Base}${input}`;
        }
        return '';
    },
}
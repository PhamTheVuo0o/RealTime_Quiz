import { BaseData } from "@ShareConstants";
import { StorageTypeEnum } from "@ShareEnums";

export const AppStorage = {
  set: (key: string, value: unknown, type?: StorageTypeEnum) => {
    switch (type) {
      case StorageTypeEnum.Local:
        AppLocalStorage.set(key, value);
        break;
      case StorageTypeEnum.Session:
        AppSessionStorage.set(key, value);
        break;
      case StorageTypeEnum.All:
      default:
        AppLocalStorage.set(key, value);
        AppSessionStorage.set(key, value);
        break;
    }
  },
  get: (key: string, type?: StorageTypeEnum): unknown => {
    switch (type) {
      case StorageTypeEnum.Local:
        return AppLocalStorage.get(key);
      case StorageTypeEnum.Session:
        return AppSessionStorage.get(key);
      case StorageTypeEnum.All:
      default: {
        const valueSession = AppSessionStorage.get(key) as any;
        const valueLocal = AppLocalStorage.get(key) as any;
        if (valueSession && valueLocal) {
          const rlt: any = {
            currentUser: valueSession.currentUser,
            users: valueLocal.users
          }
          return rlt
        }
        return valueLocal;
      }
    }
  },
  remove: (key: string, type?: StorageTypeEnum) => {
    switch (type) {
      case StorageTypeEnum.Local:
        AppLocalStorage.remove(key);
        break;
      case StorageTypeEnum.Session:
        AppSessionStorage.remove(key);
        break;
      case StorageTypeEnum.All:
      default:
        AppLocalStorage.remove(key);
        AppSessionStorage.remove(key);
        break;
    }
  },
  isDifferent: (key: string,) => {
    const valueSession = AppSessionStorage.get(key) as any;
    const valueLocal = AppLocalStorage.get(key) as any;
    if (valueSession && valueLocal) {
      return valueLocal.users != valueSession.users;
    }
  }
};

export const AppLocalStorage = {
  set: (key: string, value: unknown) => {
    window.localStorage.setItem(
      `${BaseData.APP_LOCALSTORAGE_KEY}_${key}`,
      JSON.stringify(value)
    );
  },
  get: (key: string): unknown => {
    try {
      const value = window.localStorage.getItem(
        `${BaseData.APP_LOCALSTORAGE_KEY}_${key}`
      );
      if (value) return JSON.parse(value);
    } catch (error) {
      return null;
    }
  },
  remove: (key: string) => {
    window.localStorage.removeItem(`${BaseData.APP_LOCALSTORAGE_KEY}_${key}`);
  },
};

export const AppSessionStorage = {
  set: (key: string, value: unknown) => {
    window.sessionStorage.setItem(
      `${BaseData.APP_SESSIONSTORAGE_KEY}_${key}`,
      JSON.stringify(value)
    );
  },
  get: (key: string): unknown => {
    try {
      const value = window.sessionStorage.getItem(
        `${BaseData.APP_SESSIONSTORAGE_KEY}_${key}`
      );
      if (value) return JSON.parse(value);
    } catch (error) {
      return null;
    }
  },
  remove: (key: string) => {
    window.sessionStorage.removeItem(`${BaseData.APP_SESSIONSTORAGE_KEY}_${key}`);
  },
};
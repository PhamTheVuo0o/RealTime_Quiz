export const API_URL = {
  Identity: import.meta.env.VITE_API_IDENTITY_URL,
  Core: import.meta.env.VITE_API_CORE_URL,
  MQTT: import.meta.env.VITE_MQTT_BROKER_URL,
};

export const APP_LOCALSTORAGE_KEY = 'GMS_LS';
export const APP_SESSIONSTORAGE_KEY = 'GMS_SS';

export const TITLE_MAX_LENGTH: number = 25;
export const CONTENT_MAX_LENGTH: number = 25;
export const MENU_MAX_LENGTH: number = 25;
export const DEFAULT_AVATAR:string = '/images/loading/loading_4.gif';

export const BaseData = {
  API_URL,
  APP_LOCALSTORAGE_KEY,
  TITLE_MAX_LENGTH,
  CONTENT_MAX_LENGTH,
  MENU_MAX_LENGTH,
  DEFAULT_AVATAR,
  APP_SESSIONSTORAGE_KEY
};

export default BaseData;
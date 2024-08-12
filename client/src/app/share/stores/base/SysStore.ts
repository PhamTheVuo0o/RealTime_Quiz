import { LocalStorageKeyEnum } from "@ShareEnums";
import { AppLocalStorage } from "./BaseStore";
  
  export interface SysStore {
    isLight: boolean;
    navColor: string;
    isSideBarExpand: boolean;
  }
  
  export const SysStorage = {
    set: (isLight: boolean, navColor: string, isSideBarExpand: boolean) => {
      let config = AppLocalStorage.get(
        LocalStorageKeyEnum.SystemParameter
      ) as SysStore;
      
      config = { isLight, navColor, isSideBarExpand};
  
      AppLocalStorage.set(LocalStorageKeyEnum.SystemParameter, config);
    },
    get: ():{
        config:SysStore
    } => {
      try {
        let config = AppLocalStorage.get(
          LocalStorageKeyEnum.SystemParameter
        ) as SysStore;
        if(!config) { 
            config = { isLight:true, navColor:'background-color', isSideBarExpand:true};
            AppLocalStorage.set(LocalStorageKeyEnum.SystemParameter, config);
        }
        return {config};
      } catch (error) {
        const config = { isLight:true, navColor:'background-color', isSideBarExpand:true};
        return {config};
      }
    },
    clear: () => {
      AppLocalStorage.remove(LocalStorageKeyEnum.SystemParameter);
    },
  };
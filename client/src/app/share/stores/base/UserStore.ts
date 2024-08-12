import { LocalStorageKeyEnum } from "@ShareEnums";
import { AppStorage } from "./BaseStore";
import { jwtDecode } from "jwt-decode";

export type CurrentUser = DecodedUser & {
    email: string;
    avatar: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
  };
  
  export type DecodedUser = {
    sub: string;
    id: string;
    email: string;
    preferred_username: string;
    permissions: string[];
    firstName: string;
    lastName: string;
    typ: string;
    token: string;
    selected: boolean;
    exp: number;
    organization: string;
    phone_number: string;
    avatar: string;
    isAuthenticated?: boolean;
  };
  
  export interface UserStore {
    token: string;
    email: string;
    avatar: string;
  }
  
  export interface UserConfig {
    currentUser?: string;
    users: UserStore[];
  }
  
  export const UserStorage = {
    isNeedUpdate: () => {
      return AppStorage.isDifferent(
        LocalStorageKeyEnum.SessionToken
      );
    },
    addUser: (token: string, email: string, avatar?: string) => {
      email = email.toLowerCase();
      let config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
      if (!config) config = { users: [] };
      config.currentUser = email;
  
      const users: UserStore[] = config.users?.filter((u) => u.email !== email);
      if (avatar) {
        users.push({ token, email, avatar });
      }
      else {
        if (token) {
          const tokenContent: DecodedUser = jwtDecode(token);
          if (tokenContent.avatar) {
            avatar = tokenContent.avatar;
            users.push({ token, email, avatar });
          }
        }
      }
      config.users = users;
  
      AppStorage.set(LocalStorageKeyEnum.SessionToken, config);
    },
    getUser: (email: string) => {
      email = email.toLowerCase();
      try {
        const config = AppStorage.get(
          LocalStorageKeyEnum.SessionToken
        ) as UserConfig;
        const token = config.users.find((u) => u.email === email)?.token;
        if (token) {
          return jwtDecode(token);
        } else {
          return [];
        }
      } catch (error) {
        return [];
      }
    },
    updateUser: (email: string, token?: string, avatar?: string) => {
      email = email.toLowerCase();
      const config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
      const user = config.users.find((u) => u.email === email);
      if (user) {
        if (token) user.token = token;
        if (avatar) user.avatar = avatar;
        AppStorage.set(LocalStorageKeyEnum.SessionToken, config);
      }
    },
    setCurrentUser: (email: string) => {
      email = email.toLowerCase();
      const config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
  
      if (config) {
        const user = config.users.find((u) => u.email === email);
        if (user) {
          config.currentUser = email;
          AppStorage.set(LocalStorageKeyEnum.SessionToken, config);
        }
      }
    },
    getCurrentUserToken: () => {
      const config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
      const user = config?.users?.find((u) => u.email === config?.currentUser);
  
      return user?.token;
    },
    getCurrentUserAuthStatus: () => {
      const config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
      const user = config?.users?.find((u) => u.email === config?.currentUser);
      if (user?.token) {
        const tokenContent: DecodedUser = jwtDecode(user?.token);
        if (tokenContent.exp) {
          const expt: Date = new Date(tokenContent.exp);
          const now: Date = new Date();
          const rlt: boolean = expt < now;
          return rlt;
        }
      }
      return false;
    },
    logoutCurrentUser: () => {
      const config = AppStorage.get(
        LocalStorageKeyEnum.SessionToken
      ) as UserConfig;
      if (config && config.currentUser && config.users) {
        config.users = config.users.filter(f => f.email.toLowerCase() !== config.currentUser?.toLowerCase());
        if (config.users && config.users.length > 0) {
          config.currentUser = config.users[0].email;
          AppStorage.set(LocalStorageKeyEnum.SessionToken, config);
        }
        else {
          AppStorage.remove(LocalStorageKeyEnum.SessionToken);
        }
        return true;
      }
      return false;
    },
    getTokenContent: (token: string): {
      tokenContent?: DecodedUser;
    } => {
      try {
        if (token) {
          const tokenContent: DecodedUser = jwtDecode(token);
          return { tokenContent };
        }
        return {};
      } catch (error) {
        return {};
      }
    },
    getCurrentUserTokenContent: (): {
      tokenContent?: DecodedUser;
      isAuthenticated?: boolean;
    } => {
      try {
        const config = AppStorage.get(
          LocalStorageKeyEnum.SessionToken
        ) as UserConfig;
        const user = config?.users?.find((u) => u.email === config?.currentUser);
  
        const token = user?.token;
        if (token) {
          const tokenContent: DecodedUser = jwtDecode(token);
          if (tokenContent.exp) {
            const expt: Date = new Date(tokenContent.exp);
            const now: Date = new Date();
            const isAuthenticated: boolean = expt < now;
            return { tokenContent, isAuthenticated };
          }
        }
        return {};
      } catch (error) {
        return {};
      }
    },
    getUserTokenContents: (): {
      UserTokenContents?: DecodedUser[];
    } => {
      try {
        const config = AppStorage.get(
          LocalStorageKeyEnum.SessionToken
        ) as UserConfig;
        const UserTokenContents: DecodedUser[] = [];
        config.users?.forEach(({ token, avatar }) => {
          try {
            if (token) {
              const tokenContent: DecodedUser = jwtDecode(token);
              if (tokenContent.exp) {
                const expt: Date = new Date(tokenContent.exp);
                const now: Date = new Date();
                const isAuthenticated: boolean = expt < now;
                const item: DecodedUser = {
                  ...tokenContent,
                  avatar,
                  token,
                  selected: config.currentUser?.toLowerCase() === tokenContent.email.toLowerCase(),
                  isAuthenticated
                };
                if (item) {
                  UserTokenContents.push(item);
                }
              }
            }
          } catch (error) {
            console.log("error", error);
          }
        });
  
        return { UserTokenContents };
      } catch (error) {
        return {};
      }
    },
    getParsedData: (): {
      currentUser?: CurrentUser;
      users?: DecodedUser[];
    } => {
      try {
        const config = AppStorage.get(
          LocalStorageKeyEnum.SessionToken
        ) as UserConfig;
        const users: DecodedUser[] = [];
        let currentUser;
        config.users?.forEach(({ token, avatar }) => {
          try {
            const object: DecodedUser = jwtDecode(token);
            const user = {
              ...object,
              avatar,
              token,
              selected: config.currentUser === object.email,
            };
  
            if (user.selected) {
              currentUser = { ...user };
            }
  
            users.push(user);
          } catch (error) {
            console.log("error", error);
          }
        });
  
        return { users, currentUser };
      } catch (error) {
        return {};
      }
    },
    isTokenNotExpired: (exp: number) => {
      const current_time = new Date().getTime() / 1000;
      return current_time < exp;
    },
    setItem: (data: unknown) => {
      AppStorage.set(LocalStorageKeyEnum.SessionToken, data);
    },
    clearAll: () => {
      AppStorage.remove(LocalStorageKeyEnum.SessionToken);
    },
    setCurrentUserById: (id: string) => {
      const datas = UserStorage.getUserTokenContents();
      if (datas && datas.UserTokenContents) {
        const user = datas.UserTokenContents.find((u) => u.id.toLowerCase() === id.toLowerCase());
        if (user) {
          const config = AppStorage.get(
            LocalStorageKeyEnum.SessionToken
          ) as UserConfig;
          if(config){
            config.currentUser = user.email.toLowerCase();
            AppStorage.set(LocalStorageKeyEnum.SessionToken, config);
          }
        }
      }
    },
  };
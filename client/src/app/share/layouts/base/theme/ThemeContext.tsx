import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { SysStorage } from '@ShareStores'
import { ThemeEnum } from '@ShareEnums'

interface ThemeContextType {
  isLight: boolean;
  navColor: string;
  isSideBarExpand: boolean;
  toggleIsLight: () => void;
  changeNavColor: (input: string) => void;
  toggleIsSideBarExpand: () => void;
}
export const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

interface ThemeProviderProps {
  children: ReactNode;
}

export const ThemeProvider: React.FC<ThemeProviderProps> = ({ children }) => {
  const [isLight, setIsLight] = useState(SysStorage.get().config.isLight);
  const [navColor, setNavColor] = useState(SysStorage.get().config.navColor);
  const [isSideBarExpand, setIsSideBarExpand] = useState(SysStorage.get().config.isSideBarExpand);

  useEffect(() => {
    document.documentElement.setAttribute('data-theme', isLight ? ThemeEnum.Light : ThemeEnum.Dark);
  }, [isLight, navColor, isSideBarExpand]);

  const toggleIsLight = () => {
    SysStorage.set(!isLight, navColor, isSideBarExpand);
    setIsLight(!isLight);
  };

  const changeNavColor = (input: string) => {
    setNavColor(input);
    SysStorage.set(isLight, input, isSideBarExpand);
  };

  const toggleIsSideBarExpand = () => {
    SysStorage.set(isLight, navColor, !isSideBarExpand);
    setIsSideBarExpand(!isSideBarExpand);
  };

  return (
    <ThemeContext.Provider value={{ isLight, navColor, isSideBarExpand, toggleIsLight, changeNavColor, toggleIsSideBarExpand }}>
      {children}
    </ThemeContext.Provider>
  );
};

export default ThemeContext;

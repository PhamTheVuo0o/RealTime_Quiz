import i18n from 'i18next';
import { initReactI18next, I18nextProvider } from 'react-i18next';

import {en} from '@ShareCores';
import { createContext, useContext, useState } from 'react';

i18n
  .use(initReactI18next) // passes i18n down to react-i18next
  .init({
    // the translations
    // (tip move them in a JSON file and import them,
    // or even better, manage them via a UI: https://react.i18next.com/guides/multiple-translation-files#manage-your-translations-with-a-management-gui)
    resources: {
      en,
    },
    lng: 'en', // if you're using a language detector, do not define the lng option
    fallbackLng: 'en',

    interpolation: {
      escapeValue: false, // react already safes from xss => https://www.i18next.com/translation-function/interpolation#unescape
    },
  });

const I18nContext = createContext(i18n);

export const I18nProvider = ({ children }: { children: React.ReactNode }) => {
  const [state] = useState(i18n);

  return (
    <I18nContext.Provider value={state}>
      <I18nextProvider i18n={i18n} defaultNS={'translation'}>
        {children}
      </I18nextProvider>
    </I18nContext.Provider>
  );
};

export const useI18nContext = () => useContext(I18nContext);

export default I18nProvider;

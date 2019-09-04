//    Copyright 2019 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

import i18n from 'i18next';
import fetchBackend from 'i18next-fetch-backend';
import { initReactI18next  } from 'react-i18next';

/**
 * Initializes the i18next library to provide a translation dictionary to the app.
 * If your app is not multilingual, this file and references to it can be removed.
 * Elsewhere in the app to use the dictionary `import { t } from 'i18next'; ... t('key')`
 * @param {string} language Optional, the initial language. Only used for SSR; otherwise language set in RouteHandler.
 * @param {*} dictionary Optional, the dictionary to load. Only used for SSR; otherwise, the dictionary is loaded via JSS dictionary service.
 */
export default async function i18nInit(language?: string, dictionary?: any) {
  return new Promise((resolve, reject) => {
    const options: i18n.InitOptions = {
      debug: false,
      fallbackLng: false, // fallback to keys
      interpolation: {
        escapeValue: false, // not needed for react
      },
      lng: language,
      load: 'currentOnly', // e.g. don't load 'es' when requesting 'es-MX' -- Sitecore config should handle this
      resources: {},
    };

    const initCallback = (error?: Error) => {
      if (error) {
        reject(error);
        return;
      }
      resolve();
    };

    if (dictionary && language) {
      // if we got dictionary passed, that means we're in a SSR context with a server-provided dictionary
      // so we do not want a backend, because we already know all possible keys
      options.resources = {};
      options.resources[language] = {
        translation: dictionary,
      };

      i18n.use(initReactI18next).init(options, initCallback);
    } else {
      // We're running client-side, so we get translation data from the Sitecore dictionary API using fetch backend
      // For higher performance (but less simplicity), consider adding the i18n chained backend to a local cache option like the local storage backend.

      // eslint-disable-next-line prettier/prettier
      const dictionaryServicePath = `/sitecore/api/jss/dictionary/wooli/{{lng}}?sc_apikey=${process.env.API_KEY || ''}`;

      options.backend = {
        loadPath: dictionaryServicePath,
        parse: (data: string) => {
          const parsedData = JSON.parse(data);
          if (parsedData.phrases) {
            return parsedData.phrases;
          }
          return parsedData;
        },
      };

      i18n
        .use(fetchBackend)
        .use(initReactI18next)
        .init(options, initCallback);
    }
  });
}

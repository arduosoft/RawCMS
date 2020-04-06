const _i18n = new VueI18n({
  locale: "en",
  fallbackLocale: "en",
  messages: { en: {} }
});

class I18nHelper {
  filesLoaded = [];

  load(lang, path) {
    if (this.filesLoaded.includes(path)) {
      return Promise.resolve();
    }

      try {
          return axios({
              url: path,
              method: "get"
          }).then(x => {
              this.filesLoaded.push(path);
              const messages = x.data;
              console.log(messages);
              _i18n.mergeLocaleMessage(lang, messages);
              console.log(_i18n.messages);
              return;
          });
      
      }
      catch (error) {
          console.log(error);
          return Promise.resolve();
    }
  }

  setLang(lang) {
    _i18n.locale = lang;
    axios.defaults.headers.common["Accept-Language"] = lang;
    document.querySelector("html").setAttribute("lang", lang);
  }
}

export const i18nHelper = new I18nHelper();
export const i18n = _i18n;

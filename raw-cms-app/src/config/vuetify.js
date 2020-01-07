import colors from 'https://cdn.jsdelivr.net/npm/vuetify@2.1.9/lib/util/colors.min.js';

const _vuetify = new Vuetify({
  theme: {
    themes: {
      light: {
        primary: colors.blue.darken2,
        secondary: colors.cyan.base,
        tabHeader: colors.blue.lighten2,
      },
    },
  },
});

export const vuetify = _vuetify;
export const vuetifyColors = colors;

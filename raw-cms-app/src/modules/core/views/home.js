const _Home = async (res, rej) => {
  res({
    template: `
      <v-row align="center" justify="center">
        <p>{{ $t('core.home.helpText') }}</p>
      </v-row>
      `,
  });
};

export const Home = _Home;
export default _Home;

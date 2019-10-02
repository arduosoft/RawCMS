const EntitiesDef = async (res, rej) => {
  res({
    template: `
      <v-row align="center" justify="center">
        <h1>{{ $t('core.entities.title') }}</h1>
      </v-row>
      `,
  });
};

export default EntitiesDef;

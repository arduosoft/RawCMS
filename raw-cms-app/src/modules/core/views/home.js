const _Home = async (res, rej) => {
  res({
    template: `
      <v-container class="fill-height" fluid>
        <v-row align="center" justify="center">
          <img src="/modules/core/assets/rawlogo.png">
        </v-row>
      </v-container>
      `,
  });
};

export const Home = _Home;
export default _Home;

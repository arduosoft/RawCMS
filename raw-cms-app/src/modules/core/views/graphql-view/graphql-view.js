import { RawCMS } from '/config/raw-cms.js';
const _GraphQLView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/graphql-view/graphql-view.tpl.html'
  );

  res({
    data: function() {
      return {
        graphiql: `${RawCMS.env.api.baseUrl}/graphql/`,
      };
    },
    template: tpl,
  });
};

export const GraphQLView = _GraphQLView;
export default _GraphQLView;

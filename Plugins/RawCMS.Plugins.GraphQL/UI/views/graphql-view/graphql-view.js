import { RawCMS } from "/app/config/raw-cms.js";
import vuexStore from "/app/config/vuex.js";
const _GraphQLView = async (res, rej) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/graphql/views/graphql-view/graphql-view.tpl.html"
    );

    res({
        data: function () {
            return {
                graphiql: `${RawCMS.env.api.baseUrl}/graphql/`
            };
        },
        mounted() {
            vuexStore.dispatch("core/updateTopBarTitle", this.$t("graphql.title"));
        },
        template: tpl
    });
};

export const GraphQLView = _GraphQLView;
export default _GraphQLView;
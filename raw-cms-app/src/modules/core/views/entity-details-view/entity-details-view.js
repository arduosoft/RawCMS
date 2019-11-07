import { EntityDetailsDef } from '../../components/entity-details/entity-details.js';

const _EntityDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/entity-details-view/entity-details-view.tpl.html'
  );
  const entityDetails = await EntityDetailsDef();

  res({
    components: {
      EntityDetails: entityDetails,
    },
    data: function() {
      return {};
    },
    methods: {},
    template: tpl,
  });
};

export const EntityDetailsView = _EntityDetailsView;
export default _EntityDetailsView;

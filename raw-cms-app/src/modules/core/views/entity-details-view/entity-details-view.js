import vuexStore from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { rawCmsDetailEditEvents } from '../../../shared/components/detail-edit/detail-edit.js';
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
    created: function() {
      RawCMS.eventBus.$once(rawCmsDetailEditEvents.loaded, ev => {
        this.updateTitle({
          isNew: ev.isNew,
          name: optionalChain(() => ev.value.CollectionName, { fallbackValue: '<NONE>' }),
        });
      });
    },
    methods: {
      updateTitle: function({ isNew, name }) {
        let title = isNew
          ? this.$t('core.entities.detail.newTitle')
          : this.$t('core.entities.detail.updateTitle', { name: name });
        vuexStore.dispatch('core/updateTopBarTitle', title);
      },
    },
    template: tpl,
  });
};

export const EntityDetailsView = _EntityDetailsView;
export default _EntityDetailsView;

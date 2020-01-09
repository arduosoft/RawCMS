import vuexStore from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { rawCmsDetailEditEvents } from '../../../shared/components/detail-edit/detail-edit.js';
import { ConfigurationDetailsDef } from '../../components/configuration-details/configuration-details.js';

const _ConfigurationDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/configuration-details-view/configuration-details-view.tpl.html'
  );
  const details = await ConfigurationDetailsDef();

  res({
    components: {
      ConfigurationDetails: details,
    },
    created: function() {
      RawCMS.eventBus.$on(rawCmsDetailEditEvents.loaded, ev => {
        this.updateTitle({
          isNew: ev.isNew,
          name: optionalChain(() => ev.value.plugin_name, { fallbackValue: '<NONE>' }),
        });
      });
    },
    methods: {
      updateTitle: function({ isNew, name }) {
        let title = isNew
          ? this.$t('core.configuration.detail.newTitle')
          : this.$t('core.configuration.detail.updateTitle', { name: name });

        vuexStore.dispatch('core/updateTopBarTitle', title);
      },
    },
    template: tpl,
  });
};

export const ConfigurationDetailsView = _ConfigurationDetailsView;
export default _ConfigurationDetailsView;

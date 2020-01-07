import vuexStore from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { rawCmsDetailEditEvents } from '../../../shared/components/detail-edit/detail-edit.js';
import { UserDetailsDef } from '../../components/user-details/user-details.js';

const _UserDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/user-details-view/user-details-view.tpl.html'
  );
  const details = await UserDetailsDef();

  res({
    components: {
      UserDetails: details,
    },
    created: function() {
      RawCMS.eventBus.$once(rawCmsDetailEditEvents.loaded, ev => {
        this.updateTitle({
          isNew: ev.isNew,
          name: optionalChain(() => ev.value.UserName, { fallbackValue: '<NONE>' }),
        });
      });
    },
    methods: {
      updateTitle: function({ isNew, name }) {
        const title = isNew
          ? this.$t('core.users.detail.newTitle')
          : this.$t('core.users.detail.updateTitle', { name: name });

        vuexStore.dispatch('core/updateTopBarTitle', title);
      },
    },
    template: tpl,
  });
};

export const UserDetailsView = _UserDetailsView;
export default _UserDetailsView;

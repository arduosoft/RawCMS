import vuexStore from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { rawCmsDetailEditEvents } from '../../../shared/components/detail-edit/detail-edit.js';
import { LambdaDetailsDef } from '../../components/lambda-details/lambda-details.js';

const _LambdaDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/lambda-details-view/lambda-details-view.tpl.html'
  );
  const editor = await LambdaDetailsDef();

  res({
    components: {
      LambdaDetails: editor,
    },
    created: function() {
      RawCMS.eventBus.$once(rawCmsDetailEditEvents.loaded, ev => {
        this.updateTitle({
          isNew: ev.isNew,
          name: optionalChain(() => ev.value.Name, { fallbackValue: '<NONE>' }),
        });
      });
    },
    methods: {
      updateTitle: function({ isNew, name }) {
        let title = isNew
          ? this.$t('core.lambdas.details.newTitle')
          : this.$t('core.lambdas.details.updateTitle', { name: name });

        vuexStore.dispatch('core/updateTopBarTitle', title);
      },
    },
    template: tpl,
  });
};

export const LambdaDetailsView = _LambdaDetailsView;
export default _LambdaDetailsView;

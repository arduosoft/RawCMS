import { evtToggleDrawer } from '../events.js';
import { RawCMS } from '/config/raw-cms.js';

const TopBarDef = async (resolve, reject) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/top-bar/top-bar.tpl.html');

  resolve({
    methods: {
      toggleDrawer: function() {
        RawCMS.eventBus.$emit(evtToggleDrawer);
      },
    },
    template: tpl,
  });
};

export default TopBarDef;

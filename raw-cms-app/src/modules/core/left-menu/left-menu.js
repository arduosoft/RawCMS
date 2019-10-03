import { evtToggleDrawer } from '../events.js';
import { RawCMS } from '/config/raw-cms.js';

const LeftMenuDef = async (resolve, reject) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/left-menu/left-menu.tpl.html');

  resolve({
    data: () => {
      return {
        isVisible: false,
        items: [{ icon: 'mdi-cube', text: 'Entities', route: 'entities' }],
      };
    },
    methods: {
      toggleVisibility: function() {
        this.isVisible = !this.isVisible;
      },
      goTo: function(item) {
        this.$router.replace(item.route);
      },
    },
    mounted: function() {
      RawCMS.eventBus.$on(evtToggleDrawer, () => {
        this.toggleVisibility();
      });
    },
    template: tpl,
  });
};

export default LeftMenuDef;

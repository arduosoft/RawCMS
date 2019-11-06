import { RawCMS } from '../../../../config/raw-cms.js';
import { evtToggleDrawer } from '../../events.js';

const _LeftMenu = async (resolve, reject) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/left-menu/left-menu.tpl.html'
  );

  resolve({
    data: () => {
      return {
        isVisible: false,
        items: [
          { icon: 'mdi-account', text: 'Users', route: 'users' },
          { icon: 'mdi-cube', text: 'Entities', route: 'entities' },
          { icon: 'mdi-circle', text: 'Lambdas', route: 'lambda' },
        ],
      };
    },
    methods: {
      toggleVisibility: function() {
        this.isVisible = !this.isVisible;
      },
      goTo: function(item) {
        if (this.isActive(item)) {
          return;
        }
        this.$router.push(item.route);
      },
      isActive: function(item) {
        return item.route === this.$route.name;
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

export const LeftMenu = _LeftMenu;
export default _LeftMenu;

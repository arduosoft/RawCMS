import { RawCMS } from '../../../../config/raw-cms.js';
import { vuexStore } from '../../../../config/vuex.js';
import { evtToggleDrawer } from '../../events.js';
import { loginService } from '../../services/login.service.js';
import { UserAvatarDef } from '../user-avatar/user-avatar.js';

const _LeftMenu = async (resolve, reject) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/left-menu/left-menu.tpl.html'
  );
  const avatarDef = await UserAvatarDef();

  resolve({
    components: {
      UserAvatar: avatarDef,
    },
    computed: {
      userinfo: function() {
        return vuexStore.state.core.userInfo || {};
      },
    },
    data: function() {
      return {
        isVisible: false,
        isUserMenuVisible: false,
        items: [
          { icon: 'mdi-home', text: 'Home', route: 'home' },
          { icon: 'mdi-account', text: 'Users', route: 'users' },
          { icon: 'mdi-cube', text: 'Entities', route: 'entities' },
          { icon: 'mdi-book-open', text: 'Collections', route: 'collections' },
          { icon: 'mdi-circle', text: 'Lambdas', route: 'lambdas' },
          { icon: 'mdi-settings', text: 'Configuration', route: 'plugins' },
          { icon: 'mdi-graphql', text: 'GraphQL', route: 'graphql' },
          {
            icon: 'mdi-file-document-outline',
            text: 'Dev portal',
            extLink: RawCMS.env.api.baseUrl,
          },
        ],
        bottomItem: { icon: 'mdi-information', text: 'About', route: 'about' },
      };
    },
    methods: {
      toggleVisibility: function() {
        this.isVisible = !this.isVisible;
      },
      toggleUserMenuVisibility: function() {
        this.isUserMenuVisible = !this.isUserMenuVisible;
      },
      goTo: function(item) {
        if (item === null || item === undefined) {
          return;
        }

        // Ext link
        if (item.extLink) {
          window.open(item.extLink);
          return;
        }

        // Internal route
        if (this.isActive(item)) {
          return;
        }
        this.$router.push({ name: item.route });
      },
      isActive: function(item) {
        return item.route === this.$route.name;
      },
      logout: async function() {
        loginService.logout();
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

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
    data: () => {
      return {
        isVisible: false,
        isUserMenuVisible: false,
        items: [
          { icon: 'mdi-account', text: 'Users', route: 'users' },
          { icon: 'mdi-cube', text: 'Entities', route: 'entities' },
          { icon: 'mdi-book-open', text: 'Collections', route: 'collections' },
          { icon: 'mdi-circle', text: 'Lambdas', route: 'lambda-list' },
        ],
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
        if (this.isActive(item)) {
          return;
        }
        this.$router.push({ name: item.route });
      },
      isActive: function(item) {
        vuexStore.dispatch('core/topBarTitle', this.$route.name);
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

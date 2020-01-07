import { RawCMS } from '../../../../config/raw-cms.js';
import { vuexStore } from '../../../../config/vuex.js';
import { optionalChain } from '../../../../utils/object.utils.js';
import { evtToggleDrawer } from '../../events.js';
import { UserAvatarDef } from '../user-avatar/user-avatar.js';

const _TopBar = async (resolve, reject) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/components/top-bar/top-bar.tpl.html');
  const avatarDef = await UserAvatarDef();

  resolve({
    components: {
      UserAvatar: avatarDef,
    },
    computed: {
      avatarInitials: function() {
        const userInfo = vuexStore.state.core.userInfo;
        return optionalChain(() => userInfo.UserName.substr(0, 1).toUpperCase());
      },
      username: function() {
        return vuexStore.state.core.userInfo.UserName;
      },
      Title: function() {
        return vuexStore.state.core.topBarTitle;
      },
    },
    methods: {
      toggleDrawer: function() {
        RawCMS.eventBus.$emit(evtToggleDrawer);
      },
    },
    template: tpl,
  });
};

export const TopBar = _TopBar;
export default _TopBar;

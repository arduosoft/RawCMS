import { RawCMS } from "/app/config/raw-cms.js";
import { vuexStore } from "/app/config/vuex.js";
import { optionalChain } from "/app/utils/object.utils.js";
import { evtToggleDrawer } from "/app/modules/core/events.js";
import { UserAvatarDef } from "/app/modules/core/components/user-avatar/user-avatar.js";

const _TopBar = async (resolve, reject) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/components/top-bar/top-bar.tpl.html"
    );
    const avatarDef = await UserAvatarDef();

    resolve({
        components: {
            UserAvatar: avatarDef
        },
        computed: {
            avatarInitials: function () {
                const userInfo = vuexStore.state.core.userInfo;
                return optionalChain(() =>
                    userInfo.UserName.substr(0, 1).toUpperCase()
                );
            },
            username: function () {
                return vuexStore.state.core.userInfo.UserName;
            },
            Title: function () {
                return vuexStore.state.core.topBarTitle;
            }
        },
        methods: {
            toggleDrawer: function () {
                RawCMS.eventBus.$emit(evtToggleDrawer);
            }
        },
        template: tpl
    });
};

export const TopBar = _TopBar;
export default _TopBar;
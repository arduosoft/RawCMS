import { RawCMS } from "/app/config/raw-cms.js";
import { vuexStore } from "/app/config/vuex.js";
import { optionalChain } from "/app/utils/object.utils.js";

const _UserAvatarDef = async () => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/components/user-avatar/user-avatar.tpl.html"
    );

    return {
        computed: {
            avatarInitials: function () {
                const userInfo = vuexStore.state.core.userInfo;
                return optionalChain(() =>
                    userInfo.UserName.substr(0, 1).toUpperCase()
                );
            }
        },
        props: {
            color: String,
            size: Number,
            dark: Boolean
        },
        template: tpl
    };
};

const _UserAvatar = async (res, rej) => {
    const cmpDef = _UserAvatarDef();
    res(cmpDef);
};

export const UserAvatarDef = _UserAvatarDef;
export const UserAvatar = _UserAvatar;
export default _UserAvatar;
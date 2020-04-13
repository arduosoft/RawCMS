import { RawCMS } from "/app/config/raw-cms.js";
import { vuexStore } from "/app/config/vuex.js";
import { evtToggleDrawer } from "/app/modules/core/events.js";
import { loginService } from "/app/modules/core/services/login.service.js";
import { UserAvatarDef } from "/app/modules/core/components/user-avatar/user-avatar.js";

const _LeftMenu = async (resolve, reject) => {
    const tpl = await RawCMS.loadComponentTpl(
        "/app/modules/core/components/left-menu/left-menu.tpl.html"
    );
    const avatarDef = await UserAvatarDef();

    resolve({
        components: {
            UserAvatar: avatarDef
        },
        computed: {
            userinfo: function () {
                return vuexStore.state.core.userInfo || {};
            },
            items: function () {
                let menuItems = [];
                RawCMS.env.metadata.forEach(x => {
                    var key = x.moduleName + "/menuItems";
                    var items = RawCMS.vuexStore.getters[key];
                    if (items) {
                        menuItems.push(...items);
                    }
                });

                return menuItems;
            }
        },
        data: function () {
            return {
                isVisible: false,
                isUserMenuVisible: false,

                bottomItem: { icon: "mdi-information", text: "About", route: "about" }
            };
        },
        methods: {
            toggleVisibility: function () {
                this.isVisible = !this.isVisible;
            },
            toggleUserMenuVisibility: function () {
                this.isUserMenuVisible = !this.isUserMenuVisible;
            },
            goTo: function (item) {
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
            isActive: function (item) {
                return item.route === this.$route.name;
            },
            logout: async function () {
                loginService.logout();
            }
        },
        mounted: function () {
            RawCMS.eventBus.$on(evtToggleDrawer, () => {
                this.toggleVisibility();
            });
        },
        template: tpl
    });
};

export const LeftMenu = _LeftMenu;
export default _LeftMenu;
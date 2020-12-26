import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import { userService } from '../../services/users.service.js';

const _UserDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    data: function() {
      return {
        activeTabId: 'tabFormly',
        apiService: userService,
        userRoles: this.$slots,
      };
    },
    extends: rawCmsDetailEditDef,
  };
};

const _UserDetailsDef = async () => {
  const detailWrapperDef = await _UserDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/user-details/user-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    data: function() {
      return {
        nameRules: [v => !!v || this.$t('core.users.detail.requiredNameMsg')],
        emailRules: [v => !!v || this.$t('core.users.detail.requiredEmailMsg')],
        emailRules: [v => !!v || this.$t('core.users.detail.requiredRoleslMsg')],
      };
    },
    computed: {
      userRoles: function() {
        return this.$slots;
      },
    },
    props: detailWrapperDef.extends.props,
    template: tpl,
  };
};

const _UserDetails = async (res, rej) => {
  const cmpDef = _UserDetailsDef();
  res(cmpDef);
};

export const UserDetailsDef = _UserDetailsDef;
export const UserDetails = _UserDetails;
export default _UserDetails;

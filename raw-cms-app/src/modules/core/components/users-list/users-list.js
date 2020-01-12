import { RawCmsListDef } from '../../../shared/components/list/list.js';
import { userService } from '../../services/users.service.js';

const _UsersListWrapperDef = async () => {
  const rawCmsListDef = await RawCmsListDef();

  return {
    data: function() {
      return {
        apiService: userService,
      };
    },
    extends: rawCmsListDef,
    methods: {
      deleteConfirmMsg(item) {
        return this.$t('core.users.deleteConfirmMsgTpl', { name: item.UserName });
      },
      deleteSuccessMsg(item) {
        return this.$t('core.users.deleteSuccessMsgTpl', { name: item.UserName });
      },
      deleteErrorMsg(item) {
        return this.$t('core.users.deleteErrorMsgTpl', { name: item.UserName });
      },
    },
    props: {
      detailRouteName: {
        typ: String,
        default: 'user-details',
      },
    },
  };
};

const _UsersListDef = async () => {
  const listWrapperDef = await _UsersListWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/users-list/users-list.tpl.html'
  );

  return {
    components: {
      ListWrapper: listWrapperDef,
    },
    template: tpl,
  };
};

const _UsersList = async (res, rej) => {
  const cmpDef = _UsersListDef();
  res(cmpDef);
};

export const UsersListDef = _UsersListDef;
export const UsersList = _UsersList;
export default _UsersList;

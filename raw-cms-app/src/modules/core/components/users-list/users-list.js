import { epicSpinners } from '../../../../utils/spinners.js';
import { snackbarService } from '../../services/snackbar-service.js';
import { userService } from '../../services/users.service.js';

const _UsersList = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/users-list/users-list.tpl.html'
  );

  res({
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    created: function() {
      this.fetchUsers();
    },
    data: () => {
      return {
        isLoading: true,
        users: [],
        currentUser: {},
        isDeleteConfirmVisible: false,
      };
    },
    methods: {
      fetchUsers: async function() {
        const res = await userService.getUsers();
        this.users = res.map(x => {
          return { ...x, _meta_: { isDeleting: false } };
        });
        this.isLoading = false;
      },
      goTo: function(userId) {
        this.$router.push({ name: 'user-details', params: { id: userId } });
      },
      showDeleteConfirm: function(user) {
        this.currentUser = user;
        this.isDeleteConfirmVisible = true;
      },
      dismissDeleteConfirm: function() {
        this.isDeleteConfirmVisible = false;
      },
      deleteUser: async function(user) {
        this.dismissDeleteConfirm();

        user._meta_.isDeleting = true;
        const res = await userService.deleteUser(user._id);

        user._meta_.isDeleting = false;
        if (!res) {
          snackbarService.showMessage({
            color: 'error',
            message: this.$t('core.entities.deleteErrorMsgTpl', {
              userName: user.UserName,
            }),
          });
          return;
        }

        this.users = this.users.filter(x => x._id !== user._id);
        snackbarService.showMessage({
          color: 'success',
          message: this.$t('core.entities.deletedMsgTpl', {
            userName: user.UserName,
          }),
        });
      },
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
    },
  });
};

export const UsersList = _UsersList;
export default _UsersList;

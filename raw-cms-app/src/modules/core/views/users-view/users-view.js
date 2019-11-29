const _UsersListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/users-view/users-view.tpl.html');

  res({
    template: tpl,
  });
};

export const UsersListView = _UsersListView;
export default _UsersListView;

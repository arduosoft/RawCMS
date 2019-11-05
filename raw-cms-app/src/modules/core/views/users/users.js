const _Users = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/users/users.tpl.html');

  res({
    template: tpl,
  });
};

export const users = _Users;
export default _Users;

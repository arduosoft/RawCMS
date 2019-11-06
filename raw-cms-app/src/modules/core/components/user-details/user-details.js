const _UserDetails = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/user-details/user-details.tpl.html'
  );

  res({
    props: {
      activeUser: Object,
    },
    template: tpl,
  });
};

export const UserDetails = _UserDetails;
export default _UserDetails;

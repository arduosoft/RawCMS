const _HomeView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/home-view/home-view.tpl.html');

  res({
    template: tpl,
  });
};

export const HomeView = _HomeView;
export default _HomeView;

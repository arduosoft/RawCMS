const _EntitiesView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/entities-view/entities-view.tpl.html'
  );

  res({
    template: tpl,
  });
};

export const EntitiesView = _EntitiesView;
export default _EntitiesView;

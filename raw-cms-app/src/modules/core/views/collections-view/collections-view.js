const _CollectionsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/collections-view/collections-view.tpl.html'
  );

  res({
    template: tpl,
  });
};

export const CollectionsView = _CollectionsView;
export default _CollectionsView;

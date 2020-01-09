const _ConfigurationView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/configuration-view/configuration-view.tpl.html'
  );

  res({
    template: tpl,
  });
};

export const ConfigurationView = _ConfigurationView;
export default _ConfigurationView;

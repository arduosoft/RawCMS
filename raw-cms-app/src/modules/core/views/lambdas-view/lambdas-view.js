const _LambdasView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/lambdas-view/lambdas-view.tpl.html'
  );

  res({
    template: tpl,
  });
};

export const LambdasView = _LambdasView;
export default _LambdasView;

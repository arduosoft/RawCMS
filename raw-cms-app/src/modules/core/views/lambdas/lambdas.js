const _Lambdas = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/lambdas/lambdas.tpl.html');

  res({
    template: tpl,
  });
};

export const Lambdas = _Lambdas;
export default _Lambdas;

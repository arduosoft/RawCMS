const _Entities = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl('/modules/core/views/entities/entities.tpl.html');

  res({
    template: tpl,
  });
};

export const Entities = _Entities;
export default _Entities;

const _EntityDetails = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/entity-details/entity-details.tpl.html'
  );

  res({
    props: {
      activeEntity: Object,
    },
    template: tpl,
  });
};

export const EntityDetails = _EntityDetails;
export default _EntityDetails;

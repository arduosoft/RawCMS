import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';

const _EntityDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    extends: rawCmsDetailEditDef,
  };
};

const _EntityDetailsDef = async () => {
  const detailWrapperDef = await _EntityDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/entity-details/entity-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    props: detailWrapperDef.extends.props,
    template: tpl,
  };
};

const _EntityDetails = async (res, rej) => {
  const cmpDef = _EntityDetailsDef();
  res(cmpDef);
};

export const EntityDetailsDef = _EntityDetailsDef;
export const EntityDetails = _EntityDetails;
export default _EntityDetails;

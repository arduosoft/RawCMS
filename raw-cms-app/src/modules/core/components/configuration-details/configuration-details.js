import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import { configurationService } from '../../services/configuration.service.js';

const _ConfigurationDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    data: function() {
      return {
        apiService: configurationService,
      };
    },
    extends: rawCmsDetailEditDef,
  };
};

const _ConfigurationDetailsDef = async () => {
  const detailWrapperDef = await _ConfigurationDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/configuration-details/configuration-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    props: detailWrapperDef.extends.props,
    template: tpl,
  };
};

const _ConfigurationDetails = async (res, rej) => {
  const cmpDef = _ConfigurationDetailsDef();
  res(cmpDef);
};

export const ConfigurationDetailsDef = _ConfigurationDetailsDef;
export const ConfigurationDetails = _ConfigurationDetails;
export default _ConfigurationDetails;

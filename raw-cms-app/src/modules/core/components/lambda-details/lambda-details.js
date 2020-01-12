import { RawCmsDetailEditDef } from '../../../shared/components/detail-edit/detail-edit.js';
import lambdasService from '../../services/lambdas.service.js';

const _LambdaDetailsWrapperDef = async () => {
  const rawCmsDetailEditDef = await RawCmsDetailEditDef();

  return {
    data: function() {
      return {
        activeTabId: 'tabCustom',
        apiService: lambdasService,
      };
    },
    extends: rawCmsDetailEditDef,
  };
};

const _LambdaDetailsDef = async () => {
  const detailWrapperDef = await _LambdaDetailsWrapperDef();
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/lambda-details/lambda-details.tpl.html'
  );

  return {
    components: {
      DetailWrapper: detailWrapperDef,
    },
    data: function() {
      return {
        nameRules: [
          v => !!v || this.$t('core.lambdas.details.requiredNameMsg'),
          v => (v && v.length <= 10) || this.$t('core.lambdas.details.nameTooLongMsg'),
        ],
        pathRules: [
          v => !!v || this.$t('core.lambdas.details.requiredPathMsg'),
          v => (v && v.length <= 20) || this.$t('core.lambdas.details.pathTooLongMsg'),
        ],
        customMonacoOptions: {
          language: 'javascript',
          scrollBeyondLastLine: false,
        },
      };
    },
    methods: {
      amdRequire: require,
      resizeCustomMonaco: function() {
        const monacoEditor = this.$refs.customMonaco.getMonaco();
        const oldLayout = monacoEditor.getLayoutInfo();
        const newHeight =
          this.$refs.detailWrapper.$el.getBoundingClientRect().height -
          this.$refs.tabCustom.$el.getBoundingClientRect().height -
          this.$refs.customContainer.computedStyleMap().get('padding-top').value -
          this.$refs.customContainer.computedStyleMap().get('padding-bottom').value -
          this.$refs.firstRow.getBoundingClientRect().height -
          this.$refs.monacoContainer.computedStyleMap().get('padding-top').value -
          this.$refs.monacoContainer.computedStyleMap().get('padding-bottom').value;
        monacoEditor.layout({ width: oldLayout.width, height: newHeight });
      },
    },
    props: detailWrapperDef.extends.props,
    template: tpl,
  };
};

const _LambdaDetails = async (res, rej) => {
  const cmpDef = _LambdaDetailsDef();
  res(cmpDef);
};

export const LambdaDetailsDef = _LambdaDetailsDef;
export const LambdaDetails = _LambdaDetails;
export default _LambdaDetails;

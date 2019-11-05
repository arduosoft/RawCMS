// import { snackbarService } from '../../services/snackbar-service.js';
import { epicSpinners } from '../../../../utils/spinners.js';
import { EntitiesListDef } from '../../components/entities-list/entities-list.js';

const _EntitiesListView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/entities-list-view/entities-list-view.tpl.html'
  );
  const entitiesList = await EntitiesListDef();

  res({
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
      EntitiesList: entitiesList,
    },
    data: function() {
      return {
        isLoading: false,
      };
    },
    methods: {},
    template: tpl,
  });
};

export const EntitiesListView = _EntitiesListView;
export default _EntitiesListView;

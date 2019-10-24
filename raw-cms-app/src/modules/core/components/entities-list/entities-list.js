import { epicSpinners } from '../../../../utils/spinners.js';
import { entitiesSchemaService } from '../../services/entities-schema.service.js';

const _EntitiesList = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/components/entities-list/entities-list.tpl.html'
  );

  res({
    components: {
      AtomSpinner: epicSpinners.AtomSpinner,
    },
    created: function() {
      this.fetchEntities();
    },
    data: () => {
      return {
        isLoading: true,
        entities: [],
      };
    },
    methods: {
      fetchEntities: function() {
        console.log('fetching...');
        setTimeout(async () => {
          const res = await entitiesSchemaService.getEntities();
          this.entities = res;
          this.isLoading = false;
        }, 1000);
      },
      goTo: function(entityId) {
        this.$router.push({ name: 'entity-details', params: { id: entityId } });
      },
    },
    template: tpl,
    watch: {
      $route: 'fetchData',
    },
  });
};

export const EntitiesList = _EntitiesList;
export default _EntitiesList;

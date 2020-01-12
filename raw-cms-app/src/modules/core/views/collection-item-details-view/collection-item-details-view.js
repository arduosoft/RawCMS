import { CollectionItemDetailsDef } from '../../components/collection-item-details/collection-item-details.js';

const _CollectionItemDetailsView = async (res, rej) => {
  const tpl = await RawCMS.loadComponentTpl(
    '/modules/core/views/collection-item-details-view/collection-item-details-view.tpl.html'
  );
  const collectionItemDetails = await CollectionItemDetailsDef();

  res({
    components: {
      CollectionItemDetails: collectionItemDetails,
    },
    computed: {
      collectionName: function() {
        return this.$route.params.collName;
      },
    },
    template: tpl,
  });
};

export const CollectionItemDetailsView = _CollectionItemDetailsView;
export default _CollectionItemDetailsView;

namespace RawCMS.Library.Schema.FieldTypes
{
    public class EntitiesListType : FieldType
    {
        public override string TypeName => "entities-list";

        public override FieldGraphType GraphType => FieldGraphType.String;

        public EntitiesListType()
        {
        }
    }
}
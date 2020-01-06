namespace RawCMS.Library.Schema.FieldTypes
{
    public class BoolFieldType : FieldType
    {
        public override string TypeName => "bool";

        public override FieldGraphType GraphType => FieldGraphType.Boolean;

        public BoolFieldType()
        {
        }
    }
}
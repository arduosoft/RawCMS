namespace RawCMS.Library.Schema.FieldTypes
{
    public class FieldListType : FieldType
    {
        public override string TypeName => "fields-list";

        public FieldListType()
        {
            //
            OptionParameter.Add(new OptionParameter()
            {
                Name = "Collection",
                Type = "text",
                Description = "Collection name"
            });
        }
    }
}
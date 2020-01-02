namespace RawCMS.Library.Schema.FieldTypes
{
    public class RelationFieldType : FieldType
    {
        public override string TypeName => "relation";

        public RelationFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "Collection",
                Type = "entities-list"
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "Multiple",
                Type = "int",
                Description = "1N or NM",
            });
        }
    }
}
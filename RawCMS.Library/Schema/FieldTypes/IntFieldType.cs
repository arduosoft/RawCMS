namespace RawCMS.Library.Schema.FieldTypes
{
    public class IntFieldType : FieldType
    {
        public override string TypeName => "int";

        public IntFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "max",
                Type = FieldBaseType.Int
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "min",
                Type = FieldBaseType.Int
            });
        }
    }
}
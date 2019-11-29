namespace RawCMS.Library.Schema.FieldTypes
{
    public class NumberFieldType : FieldType
    {
        public override string TypeName => "number";

        public NumberFieldType()
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
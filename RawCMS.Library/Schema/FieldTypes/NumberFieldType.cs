namespace RawCMS.Library.Schema.FieldTypes
{
    public class NumberFieldType : FieldType
    {
        public override string TypeName => "number";

        public override FieldGraphType GraphType => FieldGraphType.Decimal;

        public NumberFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "max",
                Type = "number"
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "min",
                Type = "number"
            });
        }
    }
}
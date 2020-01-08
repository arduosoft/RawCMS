namespace RawCMS.Library.Schema.FieldTypes
{
    public class IntFieldType : FieldType
    {
        public override string TypeName => "int";

        public override FieldGraphType GraphType => FieldGraphType.Int;

        public IntFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "max",
                Type = "int"
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "min",
                Type = "int"
            });
        }
    }
}
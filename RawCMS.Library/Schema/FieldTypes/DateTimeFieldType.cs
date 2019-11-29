namespace RawCMS.Library.Schema.FieldTypes
{
    public class DateTimeFieldType : FieldType
    {
        public override string TypeName => "date";

        public DateTimeFieldType()
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
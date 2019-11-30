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
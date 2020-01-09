namespace RawCMS.Library.Schema.FieldTypes
{
    public class DateTimeFieldType : FieldType
    {
        public override string TypeName => "date";

        public override FieldGraphType GraphType => FieldGraphType.Date;

        public DateTimeFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "max",
                Type = "date"
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "min",
                Type = "date"
            });
        }
    }
}
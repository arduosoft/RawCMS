namespace RawCMS.Library.Schema.FieldTypes
{
    public class TextFieldType : FieldType
    {
        public override string TypeName => "text";

        public TextFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "regexp",
                Type = FieldBaseType.String
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "maxlength",
                Type = FieldBaseType.Int
            });
        }
    }
}
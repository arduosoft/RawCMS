namespace RawCMS.Library.Schema.FieldTypes
{
    public class TextFieldType : FieldType
    {
        public override string TypeName => "text";

        public override FieldGraphType GraphType => FieldGraphType.String;

        public TextFieldType()
        {
            OptionParameter.Add(new OptionParameter()
            {
                Name = "regexp",
                Type = "text"
            });

            OptionParameter.Add(new OptionParameter()
            {
                Name = "maxlength",
                Type = "int"
            });
        }
    }
}
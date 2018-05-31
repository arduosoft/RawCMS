using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;

namespace RawCMS.Library.Schema.Validation
{
   
    public class TextValidation : FieldTypeValidator
    {
        public override string Type =>"text";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                if (field.Options["maxlength"] != null)
                {
                    int maxlenght;
                    if (int.TryParse(field.Options["maxlength"].ToString(), out maxlenght))
                    {
                        if (input[field.Name] != null && maxlenght < input[field.Name].ToString().Length)
                        {
                            errors.Add(new Error()
                            {
                                Code = "REQUIRED",
                                Title = "Field " + field.Name + " too long"
                            });
                        }
                    }
                }

            }
            return errors;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Text.RegularExpressions;

namespace RawCMS.Library.Schema.Validation
{
   
    public class TextValidation : FieldTypeValidator
    {
        public override string Type =>"text";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            string value = (input[field.Name] ?? "").ToString();
            if (field.Options != null)
            {
                if (field.Options["maxlength"] != null)
                {
                    int maxlenght;
                    if (int.TryParse(field.Options["maxlength"].ToString(), out maxlenght))
                    {
                        if (input[field.Name] != null && maxlenght < value.ToString().Length)
                        {
                            errors.Add(new Error()
                            {
                                Code = "REQUIRED",
                                Title = "Field " + field.Name + " too long"
                            });
                        }
                    }
                }

                if (field.Options["regexp"] != null)
                {
                    if (!Regex.IsMatch(value, field.Options["regexp"].ToString()))
                    {
                        errors.Add(new Error()
                        {
                            Code = "INVALID",
                            Title = "Field " + field.Name + " doesn't match" + field.Options["regexp"]
                        });
                    }
                }
            }
            return errors;
        }
    }
}

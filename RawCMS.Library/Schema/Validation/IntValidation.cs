using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Collections.Generic;

namespace RawCMS.Library.Schema.Validation
{
    public class IntValidation : FieldTypeValidator
    {
        public override string Type => "int";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                if (input[field.Name] == null)
                {
                    return errors; //null check is done on main validation
                }

                if (!int.TryParse(input[field.Name].ToString(), out int value))
                {
                    errors.Add(new Error()
                    {
                        Code = "INVALID VALUE",
                        Title = "Field " + field.Name + " has invalid format "
                    });
                    return errors;
                }

                if (field.Options["max"] != null)
                {
                    if (int.TryParse(field.Options["max"].ToString(), out int max))
                    {
                        if (input[field.Name] != null && max < value)
                        {
                            errors.Add(new Error()
                            {
                                Code = "INVALID VALUE",
                                Title = "Field " + field.Name + " too big"
                            });
                        }
                    }
                }

                if (field.Options["min"] != null)
                {
                    if (int.TryParse(field.Options["min"].ToString(), out int min))
                    {
                        if (input[field.Name] != null && min > value)
                        {
                            errors.Add(new Error()
                            {
                                Code = "INVALID VALUE",
                                Title = "Field " + field.Name + " too small"
                            });
                        }
                    }
                }
            }
            return errors;
        }
    }
}
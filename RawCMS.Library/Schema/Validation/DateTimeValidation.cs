using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;

namespace RawCMS.Library.Schema.Validation
{
   
    public class DateTimeValidation : FieldTypeValidator
    {
        public override string Type =>"int";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                if (input[field.Name] == null) return errors ; //null check is done on main validation

                DateTime value;
                if ( !DateTime.TryParse(input[field.Name].ToString(), out value))
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
                    DateTime max;
                    if (DateTime.TryParse(field.Options["max"].ToString(), out max))
                    {
                        if (input[field.Name] != null && max.Subtract(value).TotalMilliseconds<0)
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
                    DateTime min;
                    if (DateTime.TryParse(field.Options["min"].ToString(), out min))
                    {
                        if (input[field.Name] != null && min.Subtract(value).TotalMilliseconds > 0)
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

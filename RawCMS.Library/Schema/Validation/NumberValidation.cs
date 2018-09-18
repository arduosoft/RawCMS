using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;

namespace RawCMS.Library.Schema.Validation
{
   
    public class NumberValidation : FieldTypeValidator
    {
        public override string Type =>"number";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                if (input[field.Name] == null) return errors ; //null check is done on main validation

                
                if ( !double.TryParse(input[field.Name].ToString(), out double value))
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
                   
                    if (double.TryParse(field.Options["max"].ToString(), out double max))
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
                   
                    if (double.TryParse(field.Options["min"].ToString(), out double min))
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

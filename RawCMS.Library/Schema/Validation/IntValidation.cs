using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;

namespace RawCMS.Library.Schema.Validation
{
   
    public class IntValidation : FieldTypeValidator
    {
        public override string Type =>"int";

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                if (input[field.Name] == null) return errors ; //null check is done on main validation

                int value;
                if ( !int.TryParse(input[field.Name].ToString(), out value))
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
                    int max;
                    if (int.TryParse(field.Options["max"].ToString(), out max))
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
                    int min;
                    if (int.TryParse(field.Options["min"].ToString(), out min))
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

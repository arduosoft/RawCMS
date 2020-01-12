using RawCMS.Library.Schema;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Model
{
    public class FieldInfo
    {
        public FieldType Type { get; set; }
        public List<FieldClientValidation> Validations { get; set; }
    }
}
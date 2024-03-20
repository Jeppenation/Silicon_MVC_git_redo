using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class CheckboxRequired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is bool boolean && boolean;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Common
{
    public class SelectListItemExtension: SelectListItem
    {
        public string SubText { get; set; }
        public string Email { get; set; }
    }
}

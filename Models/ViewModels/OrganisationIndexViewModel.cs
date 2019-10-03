using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class OrganisationIndexItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class OrganisationIndexViewModel
    {
        public List<OrganisationIndexItemViewModel> Index = new List<OrganisationIndexItemViewModel>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class OrganisationItemIndexItemViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class OrganisationItemIndexViewModel
    {
        public string OrganisationName { get; set; }
        public string OrganisationId { get; set; }
        public List<OrganisationItemIndexItemViewModel> Index = new List<OrganisationItemIndexItemViewModel>();
    }
}

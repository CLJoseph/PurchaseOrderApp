using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Common;

namespace UI.Views.Shared.Components.HTMLSelect
{
    [ViewComponent(Name = "HTMLSelectsubtext")]
    public class HTMLSelectsubtextViewComponent : ViewComponent
    {
        public class OutputModel
        {
            // javascript function invoked when a selection change occurs 
      
            // display properties
            public string Name { get; set; }
            public string Label { get; set; }
            public string Value { get; set; }
            public string Text { get; set; }
            public string subtext { get; set; }
            public string email { get; set; }
            public string OnChangeEventHandler { get; set; }

            public List<SelectListItemExtension> Options = new List<SelectListItemExtension>();
            // initialiser
            public OutputModel()
            {}           
            public void SetOptions(string selected, List<OrgDetail>  options)
            {
                if (string.IsNullOrEmpty(selected))
                {
                    foreach (var Opt in options)
                    {
                        if (Opt.Name == selected)
                        {
                            Options.Add(new SelectListItemExtension()
                            {
                                Text = Opt.Name,
                                Value = Opt.Id,
                                Selected = true,
                                SubText = Opt.info,
                                Email = Opt.Email
                            });
                            subtext = Opt.info;
                        }
                        else
                        {
                            Options.Add(new SelectListItemExtension()
                            {
                                Text = Opt.Name,
                                Value = Opt.Id,
                                Selected = false,
                                SubText = Opt.info,
                                Email = Opt.Email

                            });
                        }
                    }
                }
                else
                {
                    foreach (var opt in options)
                    {
                        var OptSet = false;
                        if (opt.Id == selected)
                        {
                            OptSet = true;
                            subtext = opt.info;
                            email = opt.Email;
                        }
                        Options.Add(new SelectListItemExtension()
                        {
                        Text = opt.Name,
                        Value = opt.Id,
                        Selected = OptSet,
                        SubText = opt.info,
                        Email = opt.Email
                        });
                    }
                }
            }
        }
        private OutputModel _model { get; set; }

        public HTMLSelectsubtextViewComponent()
        {
            _model = new OutputModel();
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, string label, string selectedoption, List<OrgDetail> options, string  OnChangeEventHandler )
        {
            _model.Name = name;
            _model.Label = label;
            _model.OnChangeEventHandler = OnChangeEventHandler;           
            _model.SetOptions(selectedoption, options);

            if (string.IsNullOrEmpty(selectedoption))
            {
                // take 1st in list 
                _model.Value = options[0].Id;
                _model.Text = options[0].Name;
                _model.subtext = options[0].info;
                _model.email = options[0].Email;
            }

            return View("HtmlSelectsubtext", _model);
        }
    }
}
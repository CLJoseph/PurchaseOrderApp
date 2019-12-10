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
            public string Subtext { get; set; }
            public string Email { get; set; }
            public string Person { get; set; }

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
                                SubText = Opt.Info,
                                Email = Opt.Email,
                                Person = Opt.Person
                                
                            });
                            Subtext = Opt.Info;
                        }
                        else
                        {
                            Options.Add(new SelectListItemExtension()
                            {
                                Text = Opt.Name,
                                Value = Opt.Id,
                                Selected = false,
                                SubText = Opt.Info,
                                Email = Opt.Email,
                                Person = Opt.Person
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
                            Subtext = opt.Info;
                            Email = opt.Email;
                            Person = opt.Person; 
                        }
                        Options.Add(new SelectListItemExtension()
                        {
                        Text = opt.Name,
                        Value = opt.Id,
                        Selected = OptSet,
                        SubText = opt.Info,
                        Email = opt.Email,
                        Person = opt.Person
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
                _model.Subtext = options[0].Info;
                _model.Email = options[0].Email;
                _model.Person = options[0].Person;
            }

            return View("HtmlSelectsubtext", _model);
        }
    }
}
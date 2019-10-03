using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Views.Shared.Components.HTMLSelect
{
    [ViewComponent(Name = "HTMLSelect")]
    public class HTMLSelectViewComponent : ViewComponent
    {
        public class OutputModel
        {
            // javascript function invoked when a selection change occurs 
            public string OnChangeEventHandler { get; set; }
            // display properties
            public string Name { get; set; }
            public string Label { get; set; }
            public List<SelectListItem> Options = new List<SelectListItem>();
            // initialiser
            public OutputModel()
            {}           
            public void SetOptions(string selected, List<string> options)
            {
                foreach (var Opt in options)
                {
                    if (Opt == selected)
                    {
                        Options.Add(new SelectListItem()
                        {
                            Text = Opt,
                            Value = Opt,
                            Selected = true
                        });

                    }
                    else
                    {
                        Options.Add(new SelectListItem()
                        {
                            Text = Opt,
                            Value = Opt,
                            Selected = false
                        });
                    }
                }
            }
        }
        private OutputModel _model { get; set; }

        public HTMLSelectViewComponent()
        {
            _model = new OutputModel();
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, string label, string selectedoption, List<string> options, string OnChangeEventhandler)
        {
            _model.Name = name;
            _model.Label = label;
            _model.OnChangeEventHandler = OnChangeEventhandler;
            _model.SetOptions(selectedoption, options);
            return View("HtmlSelect", _model);
        }
    }
}
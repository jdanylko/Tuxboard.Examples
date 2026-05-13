using Microsoft.AspNetCore.Mvc;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Models;

namespace AsyncWidgets.Pages.Shared.Components.HelloWorld;

[ViewComponent(Name = "helloworld")]
public class HelloWorldViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement) => 
        View(new WidgetModel { Placement = placement });
}
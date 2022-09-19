using Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.ViewComponents.GalleryComponent
{
    public class UserDesignComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public UserDesignComponent(IUnitOfWork unit)
        {
            _context = unit;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return await Task.FromResult((IViewComponentResult)View("UserDesign", await _context.YourDesignUW.GetEntitiesAsync(x => x.UserId == id)));
        }
    }
}

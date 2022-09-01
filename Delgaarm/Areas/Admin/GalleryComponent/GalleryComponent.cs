using Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delgaarm.Areas.ViewComponents.GalleryComponent
{
    public class GalleryComponent: ViewComponent
    {
        private readonly IUnitOfWork _context;
        public GalleryComponent(IUnitOfWork unit)
        {
            _context= unit;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("GalleryAdmin", await _context.productGalleryUW.GetEntitiesAsync(x => x.ProductId == id)));
        }
    }
}

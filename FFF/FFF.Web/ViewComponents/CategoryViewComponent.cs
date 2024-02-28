using FFF.Core.Entities;
using FFF.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		IGenericRepository<Category> _genericRepository;
		public CategoryViewComponent(IGenericRepository<Category> genericRepository)
		{
			_genericRepository = genericRepository;
		}

		public IViewComponentResult Invoke()
		{
			return View(_genericRepository.GetAll().Include(x => x.SubCategories).OrderBy(x => x.DisplayIndex));
		}
	}
}

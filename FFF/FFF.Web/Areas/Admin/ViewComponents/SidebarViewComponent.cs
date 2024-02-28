using FFF.Core.Entities;
using FFF.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.ViewComponents
{
	public class SidebarViewComponent : ViewComponent
	{
		IGenericRepository<ContactMessages> _contactMsgRepo;
		public SidebarViewComponent(IGenericRepository<ContactMessages> contactMsgRepo)
		{
			_contactMsgRepo = contactMsgRepo;
		}

		public IViewComponentResult Invoke()
		{
			return View(_contactMsgRepo.Where(x => x.isReplied == false).ToList());
		}
	}
}

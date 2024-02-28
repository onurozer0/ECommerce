using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Services;
using FFF.Core.UnitOfWorks;
using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]


	public class ContactMessagesController : Controller
	{
		private readonly IGenericService<ContactMessages> _messagesGeneric;
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
		private readonly IUnitOfWork _unitOfWork;

		public ContactMessagesController(IMapper mapper, IGenericService<ContactMessages> messagesGeneric, IEmailService emailService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_messagesGeneric = messagesGeneric;
			_emailService = emailService;
			_unitOfWork = unitOfWork;
		}

		[Route("/admin/messages")]

		public async Task<IActionResult> Index()
		{
			var messages = await _messagesGeneric.Where(x => x.isReplied == false).ToListAsync();
			if (messages.Count() == 0)
			{
				ModelState.AddModelError(string.Empty, "Mesaj Bulunamadı!");
				return View();
			}

			var messagesVm = _mapper.Map<List<ContactMessagesViewModel>>(messages);
			return View(messagesVm);
		}
		[Route("/admin/repliedmessages")]
		public async Task<IActionResult> RepliedMessages()
		{
			var messages = await _messagesGeneric.Where(x => x.isReplied == true).ToListAsync();
			if (messages.Count() == 0)
			{
				ModelState.AddModelError(string.Empty, "Mesaj Bulunamadı!");
				return View();
			}

			var messagesVm = _mapper.Map<List<ContactMessagesViewModel>>(messages);
			return View(messagesVm);
		}
		[Route("/admin/messages/remove/{msgId}")]
		public async Task<IActionResult> RemoveMsg(int msgId)
		{
			var msg = await _messagesGeneric.GetByIdAsync(msgId);
			if (msg != null)
			{
				await _messagesGeneric.RemoveAsync(msg);
				return RedirectToAction(nameof(Index));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Mesaj Bulunamadı!");
			return View(nameof(Index));
		}
		[Route("/admin/messages/removereplied/{msgId}")]
		public async Task<IActionResult> RemoveRepliedMsg(int msgId)
		{
			var msg = await _messagesGeneric.GetByIdAsync(msgId);
			if (msg != null)
			{
				await _messagesGeneric.RemoveAsync(msg);
				return RedirectToAction(nameof(RepliedMessages));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Mesaj Bulunamadı!");
			return View(nameof(RepliedMessages));
		}
		[Route("/admin/messages/reply/{msgId}")]
		public async Task<IActionResult> ReplyMsg(int msgId)
		{
			var msg = await _messagesGeneric.GetByIdAsync(msgId);
			if (msg != null)
			{
				var msgVm = _mapper.Map<ContactMessageReplyViewModel>(msg);
				return View(msgVm);
			}
			ModelState.AddModelError(string.Empty, "Seçilen Mesaj Bulunamadı!");
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/messages/view/{msgId}")]
		public async Task<IActionResult> ViewRepliedMsg(int msgId)
		{
			var msg = await _messagesGeneric.GetByIdAsync(msgId);
			if (msg != null)
			{
				var msgVm = _mapper.Map<ContactMessageReplyViewModel>(msg);
				return View(msgVm);
			}
			ModelState.AddModelError(string.Empty, "Seçilen Mesaj Bulunamadı!");
			return RedirectToAction(nameof(RepliedMessages));
		}
		[Route("/admin/messages/reply/{msgId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ReplyMsg(ContactMessageReplyViewModel model, int msgId)
		{
			if (!ModelState.IsValid)
			{
				var message = await _messagesGeneric.GetByIdAsync(msgId);
				var msgVm = _mapper.Map<ContactMessageReplyViewModel>(message);
				return View(msgVm);
			}

			await _emailService.SendContactMsgResponse(model.ReplyMessage, model.Email);
			var msg = _mapper.Map<ContactMessages>(model);
			msg.ID = msgId;
			if (msg != null)
			{
				var msgTracked = await _messagesGeneric.GetByIdAsync(msg.ID);
				if (msgTracked != null)
				{
					msgTracked.isReplied = true;
					msgTracked.RepliedDate = DateTime.Now;
					msgTracked.ReplyMessage = model.ReplyMessage;
					await _unitOfWork.CommitAsync();
				}
			}

			TempData["Success"] = "E-Posta Başarıyla Gönderildi!";
			return RedirectToAction(nameof(Index));
		}
	}
}

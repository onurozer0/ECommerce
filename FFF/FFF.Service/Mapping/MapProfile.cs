using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.ViewModels;

namespace FFF.Service.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<UserAddresses, UserAddressesViewModel>().ReverseMap();
			CreateMap<ContactMessages, ContactMessagesViewModel>().ReverseMap();
			CreateMap<UserViewModel, AppUser>().ReverseMap();
			CreateMap<ContactMessages, ContactMessageReplyViewModel>().ReverseMap();
			CreateMap<Category, CategoryViewModel>().ReverseMap();
			CreateMap<Product, ProductViewModel>().ReverseMap();
		}
	}
}

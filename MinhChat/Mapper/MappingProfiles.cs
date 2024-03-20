using AutoMapper;
using MinhChat.Data;
using MinhChat.Hepper;
using MinhChat.Model;

namespace MinhChat.Mapper
{
    public class MappingProfiles:Profile
    {
    public MappingProfiles()
        {
            CreateMap<Data.User,Model.UserViewModel>()
                .ForMember(d => d.Username, o => o.MapFrom(src => $"{src.UserName}"));
            CreateMap<Data.Room,Model.RoomViewModel>();
            CreateMap<Data.Role,Model.RoleViewModel>();

            CreateMap<Data.Message,Model.MessageViewModel>()
                .ForMember(d => d.SenderName, o => o.MapFrom(src => $"{src.User.UserName}"))
                .ForMember(d => d.RoomName, o => o.MapFrom(src => $"{src.Room.Name}"))
                .ForMember(d => d.Avatar, o => o.MapFrom(src => $"{src.User.Avatar}"))
                .ForMember(d => d.Content, o => o.MapFrom(src => $"{src.Content}"))
                .ForMember(dst=>dst.Content, opt=>opt.MapFrom(src =>BasicEmojis.ParseEmojis(src.Content)));
            CreateMap<MessageViewModel, Message>();
        }

    }
}

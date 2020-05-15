using AutoMapper;
using Gems.DataShaping.Demo1.Dtos;
using Gems.DataShaping.Demo1.Entities;

namespace Gems.DataShaping.Demo1.Profiles
{
	public class GknParcelProfile : Profile
	{
		public GknParcelProfile()
		{
			CreateMap<GknParcel, GknParcelDto>()
				.ForMember(
					dest => dest.FullName,
					opt => opt.MapFrom(src => $"{src.KadNum} ({src.Status})"));
		}
	}
}

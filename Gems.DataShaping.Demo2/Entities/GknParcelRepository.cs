using System.Collections.Generic;

namespace Gems.DataShaping.Demo2.Entities
{
	public class GknParcelRepository
	{
		private readonly GknParcel[] GknParcels;

		public GknParcelRepository()
		{
			GknParcels = new[]
			{
				new GknParcel()
				{
					Id = 1,
					KadNum = "11:14:1203001:814",
					Area = 250,
					Status="Действующий"
				},
				new GknParcel()
				{
					Id = 2,
					KadNum = "22:14:1203001:814",
					Area = 5000,
					Status="Действующий"
				},
				new GknParcel()
				{
					Id = 3,
					KadNum = "33:14:1203001:814",
					Area = 2500,
					Status="Архивный"
				},

			}; ;
		}

		public IEnumerable<GknParcel> GetAll()
		{
			return GknParcels;
		}
	}
}

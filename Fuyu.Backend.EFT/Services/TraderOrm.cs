using Fuyu.Backend.EFT.DTO.Trading;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.Services
{
	public static class TraderOrm
	{
		public static TraderTemplate GetTraderTemplate(MongoId traderId)
		{
			return TraderDatabase.GetTraderTemplates()[traderId];
		}
	}
}

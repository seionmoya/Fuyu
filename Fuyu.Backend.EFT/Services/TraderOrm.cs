using Fuyu.Backend.BSG.Models.Trading;
using Fuyu.Common.Hashing;
using System;

namespace Fuyu.Backend.EFT.Services
{
    public class TraderOrm
    {
		public static TraderOrm Instance => instance.Value;
		private static readonly Lazy<TraderOrm> instance = new(() => new TraderOrm());

		/// <summary>
		/// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
		/// </summary>
		private TraderOrm()
		{

		}

		public static TraderTemplate GetTraderTemplate(MongoId traderId)
        {
            return TraderDatabase.Instance.GetTraderTemplates()[traderId];
        }
    }
}

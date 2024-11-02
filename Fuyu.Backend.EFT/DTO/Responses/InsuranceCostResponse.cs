﻿using System.Collections.Generic;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.EFT.DTO.Responses
{
	// { TraderId: { ItemTemplate: ItemCost } }
	public class InsuranceCostResponse : Dictionary<MongoId, Dictionary<MongoId, int>>
	{
		public InsuranceCostResponse(int capacity) : base(capacity)
		{
		}
	}
}
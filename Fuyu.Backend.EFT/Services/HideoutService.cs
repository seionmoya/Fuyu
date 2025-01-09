using System.Collections.Generic;
using Fuyu.Common.IO;
using System;
using Fuyu.Backend.BSG.Models.Locations;
using Fuyu.Backend.BSG.Models.Responses;

namespace Fuyu.Backend.EFT.Services
{
    public class HideoutService
    {
        public static HideoutService Instance => instance.Value;
        private static readonly Lazy<HideoutService> instance = new(() => new HideoutService());

        private EftOrm _eftOrm;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private HideoutService()
        {
            _eftOrm = EftOrm.Instance;
        }

        public HideoutSettingsResponse GetSettings()
        {
            return _eftOrm.GetHideoutSettings();
        }
    }
}
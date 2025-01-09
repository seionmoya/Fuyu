using System;
using Fuyu.Backend.Core.Models.Accounts;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core
{
    // NOTE: The properties of this class should _NEVER_ be accessed from the
    //       outside. Use CoreOrm instead.
    // -- seionmoya, 2024/09/06

    public class CoreDatabase
    {
        public static CoreDatabase Instance => instance.Value;
        private static readonly Lazy<CoreDatabase> instance = new(() => new CoreDatabase());

        internal readonly ThreadList<Account> Accounts;

        //                                sessid  aid
        internal readonly ThreadDictionary<string, int> Sessions;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private CoreDatabase()
        {
            Accounts = new ThreadList<Account>();
            Sessions = new ThreadDictionary<string, int>();
        }
    }
}
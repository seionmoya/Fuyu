using Fuyu.Backend.Core.Models.Accounts;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;
using System;

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

        private CoreDatabase()
        {
            Accounts = new ThreadList<Account>();
            Sessions = new ThreadDictionary<string, int>();
        }

        public void Load()
        {
            LoadAccounts();
            LoadSessions();
        }

        private void LoadAccounts()
        {
            var path = "./Fuyu/Accounts/Core/";

            if (!VFS.DirectoryExists(path))
            {
                VFS.CreateDirectory(path);
            }

            var files = VFS.GetFiles(path);

            foreach (var filepath in files)
            {
                var json = VFS.ReadTextFile(filepath);
                var account = Json.Parse<Account>(json);
                CoreOrm.Instance.SetOrAddAccount(account);

                Terminal.WriteLine($"Loaded fuyu account {account.Id}");
            }
        }

        private void LoadSessions()
        {
            // intentionally empty
            // sessions are created when users are logged in successfully
            // -- seionmoya, 2024/09/02
        }
    }
}
using System;
using Fuyu.Backend.Core.Models.Accounts;
using Fuyu.Common.Delegates;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.Core
{
    public class CoreLoader
    {
        public static CoreLoader Instance => instance.Value;
        private static readonly Lazy<CoreLoader> instance = new(() => new CoreLoader());

        private readonly CoreOrm _coreOrm;

        public LoadCallback OnLoadAccounts;
        public LoadCallback OnLoadSessions;

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private CoreLoader()
        {
            _coreOrm = CoreOrm.Instance;

            OnLoadAccounts += LoadAccounts;
            OnLoadSessions += LoadSessions;
        }

        public void Load()
        {
            OnLoadAccounts();
            OnLoadSessions();
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
                _coreOrm.SetOrAddAccount(account);

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
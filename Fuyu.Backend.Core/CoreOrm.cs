using System;
using System.Collections.Generic;
using Fuyu.Backend.Core.Models.Accounts;

namespace Fuyu.Backend.Core
{
    public class CoreOrm
    {
        public static CoreOrm Instance => instance.Value;
        private static readonly Lazy<CoreOrm> instance = new(() => new CoreOrm());

        /// <summary>
        /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
        /// </summary>
        private CoreOrm()
        {

        }

        #region Accounts
        public List<Account> GetAccounts()
        {
            return CoreDatabase.Instance.Accounts.ToList();
        }

        public Account GetAccount(string sessionId)
        {
            var accountId = GetSession(sessionId);
            if (!CoreDatabase.Instance.Accounts.TryGet(accountId, out var account))
            {
                throw new Exception($"Failed to get account with sessionID: {sessionId}");
            }

            return account;
        }

        public Account GetAccount(int accountId)
        {
            foreach (var entry in CoreDatabase.Instance.Accounts.ToList())
            {
                if (entry.Id == accountId)
                {
                    return entry;
                }
            }

            throw new Exception($"Account with {accountId} does not exist.");
        }

        public void SetOrAddAccount(Account account)
        {
            var accounts = CoreDatabase.Instance.Accounts.ToList();

            for (var i = 0; i < accounts.Count; ++i)
            {
                if (accounts[i].Id == account.Id)
                {
                    CoreDatabase.Instance.Accounts.TrySet(i, account);
                    return;
                }
            }

            CoreDatabase.Instance.Accounts.Add(account);
        }

        public void RemoveAccount(int accountId)
        {
            var accounts = CoreDatabase.Instance.Accounts.ToList();

            for (var i = 0; i < accounts.Count; ++i)
            {
                if (accounts[i].Id == accountId)
                {
                    CoreDatabase.Instance.Accounts.TryRemoveAt(i);
                    return;
                }
            }
        }
        #endregion

        #region Sessions
        public Dictionary<string, int> GetSessions()
        {
            return CoreDatabase.Instance.Sessions.ToDictionary();
        }

        public int GetSession(string sessionId)
        {
            if (!CoreDatabase.Instance.Sessions.TryGet(sessionId, out var id))
            {
                throw new Exception($"Failed to find ID for sessionId: {sessionId}");
            }

            return id;
        }

        public void SetOrAddSession(string sessionId, int accountId)
        {
            if (CoreDatabase.Instance.Sessions.ContainsKey(sessionId))
            {
                CoreDatabase.Instance.Sessions.Set(sessionId, accountId);
            }
            else
            {
                CoreDatabase.Instance.Sessions.Set(sessionId, accountId);
            }
        }

        public void RemoveSession(string sessionId)
        {
            CoreDatabase.Instance.Sessions.Remove(sessionId);
        }
        #endregion
    }
}
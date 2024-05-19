using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Data;

namespace Tai.Core.Services
{
    public class DataBase : IDataBase
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public void Read(Action<TaiDbContext> action_)
        {
            _lock.EnterReadLock();
            try
            {
                string _log = "";
                using (var db = new TaiDbContext())
                {
                    db.Database.Log = (log) =>
                    {
                        _log += log;
                    };
                    action_(db);
                }
                //Logger.Info(_log);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Write(Action<TaiDbContext> action_)
        {
            _lock.EnterWriteLock();
            string _log = string.Empty;
            try
            {
                using (var db = new TaiDbContext())
                {
                    db.Database.Log = (log) =>
                    {
                        _log += log;
                    };
                    action_(db);
                }
            }
            catch (Exception ex_)
            {
                Logger.Error(ex_);
                Logger.Info(_log);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}

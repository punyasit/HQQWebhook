using System;
using System.Collections.Generic;
using System.Text;
using HQQLibrary.Model;
using HQQLibrary.Model.Models.MaticonDB;

namespace HQQLibrary.Manager.Base
{
    public class HQQBase : IDisposable
    {
        public HelloQQDBContext Context { get; set; }

        public HQQBase()
        {
            Context = new HelloQQDBContext();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}

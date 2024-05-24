using Maxim.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Business
{
    public class LayoutService
    {
        private readonly AppDbContext _appDbContext;

        public LayoutService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //public Dictionary<string, string> GetSetting()
        //{
        //    return _appDbContext.Settings.ToDictionary
        //}
    }
}

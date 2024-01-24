using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Shared
{
    public interface IAppState
    {
        int ContributorId { get; set; }
        string ProfileId { get; set; }
        string? NickName { get; set; }
        string? Photo { get; set; }

        event Action OnCompanySelected;
    }
}

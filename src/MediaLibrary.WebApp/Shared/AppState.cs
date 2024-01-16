using MediaLibrary.WebApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.WebApp.Shared
{
    public class AppState : BindableBase, IAppState
    {
        private int _contributorId;
        public int ContributorId
        {
            get { return _contributorId; }
            set { SetProperty(ref _contributorId, value); }
        }

        private string? _nickName;
        public string? NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        
        private string? _photo;
        public string? Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        public string ProfileId { get; set; }

        public event Action OnCompanySelected;

        public void NotifyStateChanged()
        {
            OnCompanySelected?.Invoke();
        }
    }
}

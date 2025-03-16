using Microsoft.AspNetCore.Components;

namespace Apprendi.Web.Client.Pages.Account
{
    public partial class AccountLayout : LayoutComponentBase
    {
        private bool _isSidebarExpanded;
        private bool _isSmall;
        
        private string RadzenSidebarClass => _isSidebarExpanded ? "side-bar-opened sidebar" : "sidebar";
        
        private bool IsSidebarToggleVisible => _isSmall;

        private void MatchesChanged(bool value)
        {
            if (!value)
            {
                _isSidebarExpanded = false;
            }
            _isSmall = value;
        }

        private void ToggleSideBar()
        {
            _isSidebarExpanded = !_isSidebarExpanded;
        }

        private void CloseSideBar()
        {
            _isSidebarExpanded = false;
        }

        private void CloseSideBarIfSmallScreen()
        {
            if (!_isSmall) return;
            CloseSideBar();
        }
    }
}

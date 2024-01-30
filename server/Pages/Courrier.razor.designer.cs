using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using ArchiveCorr.Models.Appdb;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ArchiveCorr.Models;

namespace ArchiveCorr.Pages
{
    public partial class CourrierComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected AppdbService Appdb { get; set; }
        protected RadzenDataGrid<ArchiveCorr.Models.Appdb.Courrier> grid0;

        IEnumerable<ArchiveCorr.Models.Appdb.Courrier> _getCourriersResult;
        protected IEnumerable<ArchiveCorr.Models.Appdb.Courrier> getCourriersResult
        {
            get
            {
                return _getCourriersResult;
            }
            set
            {
                if (!object.Equals(_getCourriersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCourriersResult", NewValue = value, OldValue = _getCourriersResult };
                    _getCourriersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var appdbGetCourriersResult = await Appdb.GetCourriers(new Query() { Expand = "Documents,Structure,TypesCourrier,Category,Correspondant" });
            getCourriersResult = appdbGetCourriersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddCourrier>("Add Courrier", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(ArchiveCorr.Models.Appdb.Courrier args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCourrier>("Edit Courrier", new Dictionary<string, object>() { {"courid", args.courid} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var appdbDeleteCourrierResult = await Appdb.DeleteCourrier(data.courid);
                    if (appdbDeleteCourrierResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception appdbDeleteCourrierException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Courrier" });
            }
        }
    }
}

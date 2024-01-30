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
    public partial class CorrespondantsComponent : ComponentBase
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
        protected RadzenDataGrid<ArchiveCorr.Models.Appdb.Correspondant> grid0;

        ArchiveCorr.Models.Appdb.Correspondant _correspondant;
        protected ArchiveCorr.Models.Appdb.Correspondant correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                if (!object.Equals(_correspondant, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "correspondant", NewValue = value, OldValue = _correspondant };
                    _correspondant = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<ArchiveCorr.Models.Appdb.Correspondant> _getCorrespondantsResult;
        protected IEnumerable<ArchiveCorr.Models.Appdb.Correspondant> getCorrespondantsResult
        {
            get
            {
                return _getCorrespondantsResult;
            }
            set
            {
                if (!object.Equals(_getCorrespondantsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCorrespondantsResult", NewValue = value, OldValue = _getCorrespondantsResult };
                    _getCorrespondantsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        ArchiveCorr.Models.Appdb.Correspondant _editItem;
        protected ArchiveCorr.Models.Appdb.Correspondant editItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                if (!object.Equals(_editItem, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "editItem", NewValue = value, OldValue = _editItem };
                    _editItem = value;
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
            correspondant = new ArchiveCorr.Models.Appdb.Correspondant(){};

            var appdbGetCorrespondantsResult = await Appdb.GetCorrespondants();
            getCorrespondantsResult = appdbGetCorrespondantsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.grid0.InsertRow(new ArchiveCorr.Models.Appdb.Correspondant());
        }

        protected async System.Threading.Tasks.Task Grid0RowUpdate(dynamic args)
        {
            var appdbUpdateCorrespondantResult = await Appdb.UpdateCorrespondant(args.Id, args);
        }

        protected async System.Threading.Tasks.Task Grid0RowCreate(dynamic args)
        {
            var appdbCreateCorrespondantResult = await Appdb.CreateCorrespondant(args);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task EditButtonClick(MouseEventArgs args, dynamic data)
        {
            if (editItem != null)
            {
                var appdbCancelCorrespondantChangesResult = await Appdb.CancelCorrespondantChanges(editItem);

            }

            await this.Load();

            await this.grid0.Reload();

            this.grid0.EditRow(data);

            editItem = data;
        }

        protected async System.Threading.Tasks.Task SaveButtonClick(MouseEventArgs args, dynamic data)
        {
            this.grid0.UpdateRow(data);
        }

        protected async System.Threading.Tasks.Task CancelButtonClick(MouseEventArgs args, dynamic data)
        {
            this.grid0.CancelEditRow(data);

            var appdbCancelCorrespondantChangesResult = await Appdb.CancelCorrespondantChanges(data);
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var appdbDeleteCorrespondantResult = await Appdb.DeleteCorrespondant(data.Id);
                    if (appdbDeleteCorrespondantResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception appdbDeleteCorrespondantException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Correspondant" });
            }
        }
    }
}

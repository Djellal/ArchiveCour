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
    public partial class AddCourrierComponent : ComponentBase
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

        IEnumerable<ArchiveCorr.Models.Appdb.Structure> _getStructuresResult;
        protected IEnumerable<ArchiveCorr.Models.Appdb.Structure> getStructuresResult
        {
            get
            {
                return _getStructuresResult;
            }
            set
            {
                if (!object.Equals(_getStructuresResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStructuresResult", NewValue = value, OldValue = _getStructuresResult };
                    _getStructuresResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<ArchiveCorr.Models.Appdb.TypesCourrier> _getTypesCourriersResult;
        protected IEnumerable<ArchiveCorr.Models.Appdb.TypesCourrier> getTypesCourriersResult
        {
            get
            {
                return _getTypesCourriersResult;
            }
            set
            {
                if (!object.Equals(_getTypesCourriersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTypesCourriersResult", NewValue = value, OldValue = _getTypesCourriersResult };
                    _getTypesCourriersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<ArchiveCorr.Models.Appdb.Category> _getCategoriesResult;
        protected IEnumerable<ArchiveCorr.Models.Appdb.Category> getCategoriesResult
        {
            get
            {
                return _getCategoriesResult;
            }
            set
            {
                if (!object.Equals(_getCategoriesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriesResult", NewValue = value, OldValue = _getCategoriesResult };
                    _getCategoriesResult = value;
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

        ArchiveCorr.Models.Appdb.Courrier _courrier;
        protected ArchiveCorr.Models.Appdb.Courrier courrier
        {
            get
            {
                return _courrier;
            }
            set
            {
                if (!object.Equals(_courrier, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "courrier", NewValue = value, OldValue = _courrier };
                    _courrier = value;
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
            var appdbGetStructuresResult = await Appdb.GetStructures();
            getStructuresResult = appdbGetStructuresResult;

            var appdbGetTypesCourriersResult = await Appdb.GetTypesCourriers();
            getTypesCourriersResult = appdbGetTypesCourriersResult;

            var appdbGetCategoriesResult = await Appdb.GetCategories();
            getCategoriesResult = appdbGetCategoriesResult;

            var appdbGetCorrespondantsResult = await Appdb.GetCorrespondants();
            getCorrespondantsResult = appdbGetCorrespondantsResult;

            courrier = new ArchiveCorr.Models.Appdb.Courrier(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(ArchiveCorr.Models.Appdb.Courrier args)
        {
            try
            {
                var appdbCreateCourrierResult = await Appdb.CreateCourrier(courrier);
                DialogService.Close(courrier);
            }
            catch (System.Exception appdbCreateCourrierException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Courrier!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}

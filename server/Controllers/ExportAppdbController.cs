using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArchiveCorr.Data;

namespace ArchiveCorr
{
    public partial class ExportAppdbController : ExportController
    {
        private readonly AppdbContext context;
        private readonly AppdbService service;
        public ExportAppdbController(AppdbContext context, AppdbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Appdb/categories/csv")]
        [HttpGet("/export/Appdb/categories/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/categories/excel")]
        [HttpGet("/export/Appdb/categories/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategories(), Request.Query, false), fileName);
        }
        [HttpGet("/export/Appdb/correspondants/csv")]
        [HttpGet("/export/Appdb/correspondants/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCorrespondantsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCorrespondants(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/correspondants/excel")]
        [HttpGet("/export/Appdb/correspondants/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCorrespondantsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCorrespondants(), Request.Query, false), fileName);
        }
        [HttpGet("/export/Appdb/courriers/csv")]
        [HttpGet("/export/Appdb/courriers/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCourriersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourriers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/courriers/excel")]
        [HttpGet("/export/Appdb/courriers/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCourriersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourriers(), Request.Query, false), fileName);
        }
        [HttpGet("/export/Appdb/documents/csv")]
        [HttpGet("/export/Appdb/documents/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportDocumentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDocuments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/documents/excel")]
        [HttpGet("/export/Appdb/documents/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportDocumentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDocuments(), Request.Query, false), fileName);
        }
        [HttpGet("/export/Appdb/structures/csv")]
        [HttpGet("/export/Appdb/structures/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportStructuresToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetStructures(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/structures/excel")]
        [HttpGet("/export/Appdb/structures/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportStructuresToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetStructures(), Request.Query, false), fileName);
        }
        [HttpGet("/export/Appdb/typescourriers/csv")]
        [HttpGet("/export/Appdb/typescourriers/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportTypesCourriersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTypesCourriers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/Appdb/typescourriers/excel")]
        [HttpGet("/export/Appdb/typescourriers/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportTypesCourriersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTypesCourriers(), Request.Query, false), fileName);
        }
    }
}

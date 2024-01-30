using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using ArchiveCorr.Data;

namespace ArchiveCorr
{
    public partial class AppdbService
    {
        AppdbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly AppdbContext context;
        private readonly NavigationManager navigationManager;

        public AppdbService(AppdbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriesRead(ref IQueryable<Models.Appdb.Category> items);

        public async Task<IQueryable<Models.Appdb.Category>> GetCategories(Query query = null)
        {
            var items = Context.Categories.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoryCreated(Models.Appdb.Category item);
        partial void OnAfterCategoryCreated(Models.Appdb.Category item);

        public async Task<Models.Appdb.Category> CreateCategory(Models.Appdb.Category category)
        {
            OnCategoryCreated(category);

            var existingItem = Context.Categories
                              .Where(i => i.Id == category.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(category).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoryCreated(category);

            return category;
        }
        public async Task ExportCorrespondantsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/correspondants/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/correspondants/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCorrespondantsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/correspondants/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/correspondants/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCorrespondantsRead(ref IQueryable<Models.Appdb.Correspondant> items);

        public async Task<IQueryable<Models.Appdb.Correspondant>> GetCorrespondants(Query query = null)
        {
            var items = Context.Correspondants.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCorrespondantsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCorrespondantCreated(Models.Appdb.Correspondant item);
        partial void OnAfterCorrespondantCreated(Models.Appdb.Correspondant item);

        public async Task<Models.Appdb.Correspondant> CreateCorrespondant(Models.Appdb.Correspondant correspondant)
        {
            OnCorrespondantCreated(correspondant);

            var existingItem = Context.Correspondants
                              .Where(i => i.Id == correspondant.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Correspondants.Add(correspondant);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(correspondant).State = EntityState.Detached;
                throw;
            }

            OnAfterCorrespondantCreated(correspondant);

            return correspondant;
        }
        public async Task ExportCourriersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/courriers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/courriers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCourriersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/courriers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/courriers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCourriersRead(ref IQueryable<Models.Appdb.Courrier> items);

        public async Task<IQueryable<Models.Appdb.Courrier>> GetCourriers(Query query = null)
        {
            var items = Context.Courriers.AsQueryable();

            items = items.Include(i => i.Structure);

            items = items.Include(i => i.TypesCourrier);

            items = items.Include(i => i.Category);

            items = items.Include(i => i.Correspondant);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCourriersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourrierCreated(Models.Appdb.Courrier item);
        partial void OnAfterCourrierCreated(Models.Appdb.Courrier item);

        public async Task<Models.Appdb.Courrier> CreateCourrier(Models.Appdb.Courrier courrier)
        {
            OnCourrierCreated(courrier);

            var existingItem = Context.Courriers
                              .Where(i => i.courid == courrier.courid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Courriers.Add(courrier);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(courrier).State = EntityState.Detached;
                courrier.Structure = null;
                courrier.TypesCourrier = null;
                courrier.Category = null;
                courrier.Correspondant = null;
                throw;
            }

            OnAfterCourrierCreated(courrier);

            return courrier;
        }
        public async Task ExportDocumentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDocumentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDocumentsRead(ref IQueryable<Models.Appdb.Document> items);

        public async Task<IQueryable<Models.Appdb.Document>> GetDocuments(Query query = null)
        {
            var items = Context.Documents.AsQueryable();

            items = items.Include(i => i.Courrier);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDocumentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDocumentCreated(Models.Appdb.Document item);
        partial void OnAfterDocumentCreated(Models.Appdb.Document item);

        public async Task<Models.Appdb.Document> CreateDocument(Models.Appdb.Document document)
        {
            OnDocumentCreated(document);

            var existingItem = Context.Documents
                              .Where(i => i.Id == document.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Documents.Add(document);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(document).State = EntityState.Detached;
                document.Courrier = null;
                throw;
            }

            OnAfterDocumentCreated(document);

            return document;
        }
        public async Task ExportStructuresToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/structures/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/structures/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportStructuresToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/structures/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/structures/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnStructuresRead(ref IQueryable<Models.Appdb.Structure> items);

        public async Task<IQueryable<Models.Appdb.Structure>> GetStructures(Query query = null)
        {
            var items = Context.Structures.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnStructuresRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStructureCreated(Models.Appdb.Structure item);
        partial void OnAfterStructureCreated(Models.Appdb.Structure item);

        public async Task<Models.Appdb.Structure> CreateStructure(Models.Appdb.Structure structure)
        {
            OnStructureCreated(structure);

            var existingItem = Context.Structures
                              .Where(i => i.Id == structure.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Structures.Add(structure);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(structure).State = EntityState.Detached;
                throw;
            }

            OnAfterStructureCreated(structure);

            return structure;
        }
        public async Task ExportTypesCourriersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/typescourriers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/typescourriers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTypesCourriersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/appdb/typescourriers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/appdb/typescourriers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTypesCourriersRead(ref IQueryable<Models.Appdb.TypesCourrier> items);

        public async Task<IQueryable<Models.Appdb.TypesCourrier>> GetTypesCourriers(Query query = null)
        {
            var items = Context.TypesCourriers.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTypesCourriersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTypesCourrierCreated(Models.Appdb.TypesCourrier item);
        partial void OnAfterTypesCourrierCreated(Models.Appdb.TypesCourrier item);

        public async Task<Models.Appdb.TypesCourrier> CreateTypesCourrier(Models.Appdb.TypesCourrier typesCourrier)
        {
            OnTypesCourrierCreated(typesCourrier);

            var existingItem = Context.TypesCourriers
                              .Where(i => i.Id == typesCourrier.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TypesCourriers.Add(typesCourrier);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(typesCourrier).State = EntityState.Detached;
                throw;
            }

            OnAfterTypesCourrierCreated(typesCourrier);

            return typesCourrier;
        }

        partial void OnCategoryDeleted(Models.Appdb.Category item);
        partial void OnAfterCategoryDeleted(Models.Appdb.Category item);

        public async Task<Models.Appdb.Category> DeleteCategory(int? id)
        {
            var itemToDelete = Context.Categories
                              .Where(i => i.Id == id)
                              .Include(i => i.Courriers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoryDeleted(itemToDelete);

            Context.Categories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoryDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCategoryGet(Models.Appdb.Category item);

        public async Task<Models.Appdb.Category> GetCategoryById(int? id)
        {
            var items = Context.Categories
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnCategoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.Category> CancelCategoryChanges(Models.Appdb.Category item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoryUpdated(Models.Appdb.Category item);
        partial void OnAfterCategoryUpdated(Models.Appdb.Category item);

        public async Task<Models.Appdb.Category> UpdateCategory(int? id, Models.Appdb.Category category)
        {
            OnCategoryUpdated(category);

            var itemToUpdate = Context.Categories
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(category);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCategoryUpdated(category);

            return category;
        }

        partial void OnCorrespondantDeleted(Models.Appdb.Correspondant item);
        partial void OnAfterCorrespondantDeleted(Models.Appdb.Correspondant item);

        public async Task<Models.Appdb.Correspondant> DeleteCorrespondant(int? id)
        {
            var itemToDelete = Context.Correspondants
                              .Where(i => i.Id == id)
                              .Include(i => i.Courriers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCorrespondantDeleted(itemToDelete);

            Context.Correspondants.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCorrespondantDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCorrespondantGet(Models.Appdb.Correspondant item);

        public async Task<Models.Appdb.Correspondant> GetCorrespondantById(int? id)
        {
            var items = Context.Correspondants
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnCorrespondantGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.Correspondant> CancelCorrespondantChanges(Models.Appdb.Correspondant item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCorrespondantUpdated(Models.Appdb.Correspondant item);
        partial void OnAfterCorrespondantUpdated(Models.Appdb.Correspondant item);

        public async Task<Models.Appdb.Correspondant> UpdateCorrespondant(int? id, Models.Appdb.Correspondant correspondant)
        {
            OnCorrespondantUpdated(correspondant);

            var itemToUpdate = Context.Correspondants
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(correspondant);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCorrespondantUpdated(correspondant);

            return correspondant;
        }

        partial void OnCourrierDeleted(Models.Appdb.Courrier item);
        partial void OnAfterCourrierDeleted(Models.Appdb.Courrier item);

        public async Task<Models.Appdb.Courrier> DeleteCourrier(Guid? courid)
        {
            var itemToDelete = Context.Courriers
                              .Where(i => i.courid == courid)
                              .Include(i => i.Documents)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourrierDeleted(itemToDelete);

            Context.Courriers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourrierDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCourrierGet(Models.Appdb.Courrier item);

        public async Task<Models.Appdb.Courrier> GetCourrierBycourid(Guid? courid)
        {
            var items = Context.Courriers
                              .AsNoTracking()
                              .Where(i => i.courid == courid);

            items = items.Include(i => i.Structure);

            items = items.Include(i => i.TypesCourrier);

            items = items.Include(i => i.Category);

            items = items.Include(i => i.Correspondant);

            var itemToReturn = items.FirstOrDefault();

            OnCourrierGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.Courrier> CancelCourrierChanges(Models.Appdb.Courrier item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourrierUpdated(Models.Appdb.Courrier item);
        partial void OnAfterCourrierUpdated(Models.Appdb.Courrier item);

        public async Task<Models.Appdb.Courrier> UpdateCourrier(Guid? courid, Models.Appdb.Courrier courrier)
        {
            OnCourrierUpdated(courrier);

            var itemToUpdate = Context.Courriers
                              .Where(i => i.courid == courid)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(courrier);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCourrierUpdated(courrier);

            return courrier;
        }

        partial void OnDocumentDeleted(Models.Appdb.Document item);
        partial void OnAfterDocumentDeleted(Models.Appdb.Document item);

        public async Task<Models.Appdb.Document> DeleteDocument(int? id)
        {
            var itemToDelete = Context.Documents
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDocumentDeleted(itemToDelete);

            Context.Documents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDocumentDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnDocumentGet(Models.Appdb.Document item);

        public async Task<Models.Appdb.Document> GetDocumentById(int? id)
        {
            var items = Context.Documents
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Courrier);

            var itemToReturn = items.FirstOrDefault();

            OnDocumentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.Document> CancelDocumentChanges(Models.Appdb.Document item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDocumentUpdated(Models.Appdb.Document item);
        partial void OnAfterDocumentUpdated(Models.Appdb.Document item);

        public async Task<Models.Appdb.Document> UpdateDocument(int? id, Models.Appdb.Document document)
        {
            OnDocumentUpdated(document);

            var itemToUpdate = Context.Documents
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(document);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterDocumentUpdated(document);

            return document;
        }

        partial void OnStructureDeleted(Models.Appdb.Structure item);
        partial void OnAfterStructureDeleted(Models.Appdb.Structure item);

        public async Task<Models.Appdb.Structure> DeleteStructure(int? id)
        {
            var itemToDelete = Context.Structures
                              .Where(i => i.Id == id)
                              .Include(i => i.Courriers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnStructureDeleted(itemToDelete);

            Context.Structures.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterStructureDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnStructureGet(Models.Appdb.Structure item);

        public async Task<Models.Appdb.Structure> GetStructureById(int? id)
        {
            var items = Context.Structures
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnStructureGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.Structure> CancelStructureChanges(Models.Appdb.Structure item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnStructureUpdated(Models.Appdb.Structure item);
        partial void OnAfterStructureUpdated(Models.Appdb.Structure item);

        public async Task<Models.Appdb.Structure> UpdateStructure(int? id, Models.Appdb.Structure structure)
        {
            OnStructureUpdated(structure);

            var itemToUpdate = Context.Structures
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(structure);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterStructureUpdated(structure);

            return structure;
        }

        partial void OnTypesCourrierDeleted(Models.Appdb.TypesCourrier item);
        partial void OnAfterTypesCourrierDeleted(Models.Appdb.TypesCourrier item);

        public async Task<Models.Appdb.TypesCourrier> DeleteTypesCourrier(int? id)
        {
            var itemToDelete = Context.TypesCourriers
                              .Where(i => i.Id == id)
                              .Include(i => i.Courriers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTypesCourrierDeleted(itemToDelete);

            Context.TypesCourriers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTypesCourrierDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTypesCourrierGet(Models.Appdb.TypesCourrier item);

        public async Task<Models.Appdb.TypesCourrier> GetTypesCourrierById(int? id)
        {
            var items = Context.TypesCourriers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnTypesCourrierGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Appdb.TypesCourrier> CancelTypesCourrierChanges(Models.Appdb.TypesCourrier item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTypesCourrierUpdated(Models.Appdb.TypesCourrier item);
        partial void OnAfterTypesCourrierUpdated(Models.Appdb.TypesCourrier item);

        public async Task<Models.Appdb.TypesCourrier> UpdateTypesCourrier(int? id, Models.Appdb.TypesCourrier typesCourrier)
        {
            OnTypesCourrierUpdated(typesCourrier);

            var itemToUpdate = Context.TypesCourriers
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(typesCourrier);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterTypesCourrierUpdated(typesCourrier);

            return typesCourrier;
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Identity;

namespace ArchiveCorr.Layouts
{
    public partial class MainLayoutComponent
    {
        public async Task EnsureRolesAsync()
        {
            var roles = await Security.GetRoles();

            if (!roles.Any(r => r.Name == Constants.admin))
                await Security.CreateRole(new IdentityRole(Constants.admin));
            if (!roles.Any(r => r.Name == Constants.structadmin))
                await Security.CreateRole(new IdentityRole(Constants.structadmin));
            if (!roles.Any(r => r.Name == Constants.user))
                await Security.CreateRole(new IdentityRole(Constants.user));


        }
    }
}

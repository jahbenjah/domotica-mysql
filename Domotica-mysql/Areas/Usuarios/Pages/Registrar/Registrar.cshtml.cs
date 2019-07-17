using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domotica_mysql.Data.CustomIdentity;
using Domotica_mysql.Data;
using Domotica_mysql.Library;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Domotica_mysql.Areas.Usuarios.Models;
using Microsoft.AspNetCore.Http;

namespace Domotica_mysql.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        private ListObject objeto = new ListObject();
        public RegistrarModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager,
           ApplicationDbContext context, IHostingEnvironment environment)
        {
            objeto._roleManager = roleManager;
            objeto._userManager = userManager;
            objeto._environment = environment;
            objeto._context = context;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UserRoles();
            objeto._image = new UploadImage();
            objeto._userRoles = new List<SelectListItem>();
        }
        public void OnGet()
        {
            //para obtener los roles de usuario SuperAdmin,Admin, User,
            Input = new InputModel
            {
                rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
            };

        }
        //enlazamos las propiedades del front end con las del backend 
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : InputModelRegistrar
        {
            [TempData]
            public string ErrorMessage { get; set; }

            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
    }
}
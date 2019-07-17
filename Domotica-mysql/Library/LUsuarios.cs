using Domotica_mysql.Areas.Identity.Pages.Account;
using Domotica_mysql.Data.CustomIdentity;
using Domotica_mysql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domotica_mysql.Data.CustomIdentity;
using Microsoft.AspNetCore.Identity;
using Sistem_Ventas.Library;
using Domotica_mysql.Areas.Usuarios.Models;

namespace Domotica_mysql.Library
{
    public class LUsuarios : ListObject
    {
        public LUsuarios()
        {

        }
        public LUsuarios(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRole = new UserRoles();
        }
        public LUsuarios(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRole = new UserRoles();
        }
        //este método  es para poder verlo en una página razor por tanto me hace falta trabajar con los model
        
        public async Task<List<InputModelRegistrar>> GetUsuariosAsync(String valor)
        {
            List<ApplicationUser> Users;
            Users = _context.Users.ToList();

            //lista de la tabla AspNetUsers
            List<ApplicationUser> ListUsers;

            //lista de la tabla Usuarios
            List<Usuarios> ListUsuarios;
          
            ListUsuarios = _context.Usuarios.ToList();
            if (valor != null)
            {
                //Listar los usuarios de la tabla AspNetUsers 
                ListUsers = Users.Where(u => u.Email.StartsWith(valor) || u.UserName.StartsWith(valor) ||
                    u.PhoneNumber.StartsWith(valor)).ToList();
                //relacionar el email con el Id
                var user = ListUsers.First();
                var usuario = ListUsuarios.Where(u => u.ApplicationUserId == user.Id);
                //parts.Find(x => x.PartName.Contains("seat")));
                //coger el ID del usuario para añadir los datos de ese usuario en donde corresponde

            }
            else
            {
                ListUsers = _context.Users.ToList();
                ListUsuarios = _context.Usuarios.ToList();
            }
            
            //lista de usuarios además utilizando datos de la otra tabla 
            List<InputModelRegistrar> userList = new List<InputModelRegistrar>();
            foreach (var item in ListUsers)
            {
                
                userList.Add(new InputModelRegistrar
                {

                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    
                    
                });
            }
            foreach (var item in ListUsuarios)
            {
                _userRoles = await _usersRole.GetRole(_userManager, _roleManager, item.ApplicationUserId);
                userList.Add(new InputModelRegistrar
                {
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    DNI = item.DNI,
                    Imagen = item.Imagen
                    
                });
            }

            return userList;
        }
        
    }
}

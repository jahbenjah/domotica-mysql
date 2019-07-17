using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using System.Threading.Tasks;
using Domotica_mysql.Areas.Usuarios.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Domotica_mysql.Library
{
    public class UploadImage
    {
        public string carpeta;
        public async Task copiarImagenAsync(IFormFile AvatarImage, string imageName, IHostingEnvironment environment, string carpeta,
            String imagen)
        {
            if (null == AvatarImage)
            {
                String archivoOrigen;
                //vamos a copiar la imagen AvatarImage para destFileName 
                //string sAvatarImage = AvatarImage.FileName;
                //Stream strAvatarImage = AvatarImage.OpenReadStream();
                //var destAvatarImage = environment.ContentRootPath + "\\wwwroot\\images\\fotos\\" + carpeta + "\\" + sAvatarImage;

                var destFileName = environment.ContentRootPath + "\\wwwroot\\images\\fotos\\" + carpeta + "\\" + imageName;
                //File.Copy(destAvatarImage, destFileName, true);
                //File.Copy(sAvatarImage, destFileName, true);
                //guardamos en una variable si es nula y ponemos esta foto 

                /* si esto es igual a null estamos en el modo de insertar un usuario y 
                 * no es necesario obtener la imagen 
                 */
                //no hemos seleccionado ninguna imagen sera == null si hemos seleccionado imagen no sera null
                if (imagen == null)
                {
                    /*esto lo realizare desde otra logica, y es que si el usuario no selecciona imagen, todos usaran la
                     *misma imagen por defecto y cargaran la misma desde le mismo sitio sin tener que copiarla.
                    */
                    archivoOrigen = environment.ContentRootPath + "\\wwwroot\\images\\fotos\\" + carpeta + "\\default.png";
                    //en caso de null copia la imagen default.png y nos quedamos con esa imagen
                    File.Copy(archivoOrigen, destFileName, true);
                }
                else
                {
                    //archivoOrigen = environment.ContentRootPath + "/wwwroot/images/fotos/" +
                    //    carpeta + "/" + imagen + ".png";
                    archivoOrigen = environment.ContentRootPath + "\\wwwroot\\images\\fotos\\" + carpeta + "\\default.png";
                    //Si son distintos vamos a actualizar la imagen y el correo electrónico del usuario 
                    if (imageName != imagen + ".png")
                    {
                        //copiamos la imagen 
                        File.Copy(archivoOrigen, destFileName, true);
                        File.Delete(archivoOrigen);
                    }
                    else
                    {

                    }
                }


                // primer parametro direccion del archivo que vamos a copiar 

            }
            else
            {
                var filePath = Path.Combine(environment.ContentRootPath, "wwwroot\\images\\fotos\\" + carpeta, imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarImage.CopyToAsync(stream);
                }
            }

        } //fin método CopiarImagenAsync 
        public void deleteImagenAsync(IHostingEnvironment environment, String carpeta, String imagen)
        {
            var archivoOrigen = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/" + imagen + ".png";
            File.Delete(archivoOrigen);
        }
    }
}

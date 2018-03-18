using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeRecipes.Models
{
    public class Recipe
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The Name of recipe should be betwen 5 and 100 characters.")]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Directions { get; set; }
        
        [Required]
        public string Ingredients { get; set; }

        public IEnumerable<string> DirectionsList
        {
            get { return (Directions ?? string.Empty).Split(Environment.NewLine); }
        }

        public IEnumerable<string> IngredientsList
        {
            get { return (Ingredients ?? string.Empty).Split(Environment.NewLine); }
        }

        #region Image

        public byte[] Image { get; set; }

        public string ImageContentType { get; set; }

        public string GetInlineImageSrc ()
        {
            if (Image == null || ImageContentType == null)
                return null;

            var base64Image = System.Convert.ToBase64String(Image);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        public void SetImage(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null)
                return;

            ImageContentType = file.ContentType;

            using (var stream = new System.IO.MemoryStream())
            {
                file.CopyTo(stream);
                Image = stream.ToArray();
            }
        }

        #endregion
    }
}
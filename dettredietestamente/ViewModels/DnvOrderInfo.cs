using System.ComponentModel.DataAnnotations;

namespace DttInfo.ViewModels
{
    public class DnvOrderInfo
    {
        [Display(Name = "Dit navn")]
        [Required(ErrorMessage = "Vær venlig at indtaste dit navn")]
        public string FullName { get; set; }
   
        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Vær venlig at indtaste din adresse")]
        public string Address { get; set; }

        [Display(Name = "Postnr og By")]
        [Required(ErrorMessage = "Vær venlig at indtaste postnummer og by")]
        public string ZipTown { get; set; }
        
        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Vær venlig at indtaste dit telefonnummer")]
        public string Phone { get; set; }

        [Display(Name = "Din email")]
        [Required(ErrorMessage = "Vær venlig at indtaste din email adresse")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
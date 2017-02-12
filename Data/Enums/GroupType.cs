using TheGodfatherGM.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Data.Enums
{
    public enum GroupType
    {
        [Display(Name = "Police Department")]
        [BlipType(60)]
        PoliceDepartment = 0,

        [Display(Name = "FBI")]
        [BlipType(60)]
        FBI = 1,

        [Display(Name = "National Guard")]
        [BlipType(60)]
        NationalGuard = 2,

        [Display(Name = "Medical Department")]
        [BlipType(61)]
        MedicalDepartment = 3,

        [Display(Name = "Correctional Facitility")]
        [BlipType(60)]
        CorrectionalFacility = 4,

        [Display(Name = "Hitman Agency")]
        HitmanAgency = 5,

        [Display(Name = "Taxi Cab Company")]
        [BlipType(198)]
        TaxiCabCompany = 6,

        [Display(Name = "News Network")]
        [BlipType(50)]
        NewsNetwork = 7,

        [Display(Name = "Criminal Organization")]
        [BlipType(1)]
        CriminalOrganization = 8,

        [Display(Name = "Business")]
        [BlipType(50)]
        Business = 9
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductManagementMVC.Models.EntityFramework;

namespace ProductManagementMVC.ViewMoldes
{
    public class UrunFormViewMoleds
    {
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public Urun Urun{ get; set; }
    }
}
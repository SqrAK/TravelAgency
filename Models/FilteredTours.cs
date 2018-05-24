using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelAgency.Models
{
    public class FilteredTours
    {
        // Список тем
        public IEnumerable<Tour> Tours { get; set; }
        // Выбранные условия фильтра
        public int? SelectedToursCountry { get; set; }
        public int? SelectedResort { get; set; }
        public int? SelectedStar { get; set; }
        public bool Free { get; set; }
        // Элементы формы
        public SelectList Country { get; set; }
        public SelectList CountryResort { get; set; }
        // Инфа для пагинации
        public PageInfo PageInfo { get; set; }
    }
}
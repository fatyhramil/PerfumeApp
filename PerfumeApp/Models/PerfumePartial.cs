using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeApp.Models
{
    public partial class Perfume
    {
        public string FullImagePath
        {
            get
            {
                if (PhotoName == null || PhotoName == "" || PhotoName == "нет")
                {
                    return "/Images/picture.png";
                }
                else
                {
                    return "/Images"+PhotoName;
                }
            }
        }

        public decimal FullPrice
        {
            get
            {
                if (Price<600)
                {
                    return 777;
                }
                else
                {
                    return (decimal)Price;
                }
            }
        }
    }
}

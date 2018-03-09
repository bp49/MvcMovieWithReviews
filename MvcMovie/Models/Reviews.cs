using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Reviews
    {
        public int ReviewsID { get; set; }

        public string Reviewer { get; set; }

        public string Review { get; set; }

        public int MovieID { get; set; }

        public Movie Movie { get; set; }


    }
}

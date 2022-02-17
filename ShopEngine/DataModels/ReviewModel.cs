using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopEngine.DataModels
{
    public class ReviewModel
    {
        public int RevID { get; set; }

        public string bylogin { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public int idApp { get; set; }
    }
}

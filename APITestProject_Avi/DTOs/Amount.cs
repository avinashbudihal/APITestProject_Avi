using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APITestProject_Avi.DTOs
{
    public class Amount
    {
        /// <summary>
        /// double datatype is used to handle wide range of numbers
        /// </summary>
        public double amount { get; set; }
    }
}

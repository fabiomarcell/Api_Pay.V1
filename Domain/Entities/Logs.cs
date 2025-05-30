using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public static class Logs
    {
        private static List<string> Content { get; set; } = new List<string>();

        public static void AdicionarLog(string content)
        {
            Content.Add(content);
        }
        public static string[] ConsultarLog()
        {
            return Content.ToArray();
        }
    }
}

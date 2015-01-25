using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Prog();
            prog.rules = new List<EnabledRule> {
                new EnabledRule ( "RegNumber", "Status", "Equal", 1),
                new EnabledRule ( "RegNumber", "Status", "Equal", 2),
            
            };
            
            Document doc = new Document();
            doc.Status = 1;
            Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            doc = new Document();
            doc.Status = 2;
            Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            doc = new Document();
            doc.Status = 3;
            Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            
            Console.ReadLine();
        }
        
    }
}

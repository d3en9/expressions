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
            //var prog = new Prog();
            //prog.rules = new List<EnabledRule> {
            //    new EnabledRule ( "RegNumber", "Status", "Equal", 1),
            //    new EnabledRule ( "RegNumber", "Status", "Equal", 2),
            
            //};

            var filters = new List<FilterRule>
            {
                new FilterRule("RegNumber", "Equal", "2"),
                new FilterRule("Status", "Equal", 2),
            };

            var docs = new List<Document> {
                new Document() { RegNumber = "1", Status=1 },
                new Document() { RegNumber = "2", Status=2 },
                new Document() { RegNumber = "3", Status=3 },
            };

            var filter = new FilterExpression<Document>();
            var result = filter.Filter(docs, filters);

            //Document doc = new Document();
            //doc.Status = 1;
            //Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            //doc = new Document();
            //doc.Status = 2;
            //Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            //doc = new Document();
            //doc.Status = 3;
            //Console.WriteLine(prog.GetEnabled(doc, "RegNumber"));
            
            Console.ReadLine();
        }
        
    }
}

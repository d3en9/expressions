using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions
{
    public class Document
    {
        public string RegNumber { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }

    public class Prog
    {
        public List<EnabledRule> rules;

        public bool GetEnabled(Document doc, string memberName)
        {
            var compiledRules = rules.Where(y => y.MemberName == memberName).Select(r => CompileRule(r)).ToList();
            var result = compiledRules.Any(rule => rule(doc));
            return result;
        }

        public Func<Document, bool> CompileRule(EnabledRule r)
        {
            var paramDoc = Expression.Parameter(typeof(Document));
            Expression expr = BuildExpr(r, paramDoc);
            return Expression.Lambda<Func<Document, bool>>(expr, paramDoc).Compile();
        }

        Expression BuildExpr(EnabledRule r, ParameterExpression paramDoc)
        {
            var left = MemberExpression.Property(paramDoc, r.StatusName);
            ExpressionType tBinary;
            // is the operator a known .NET operator?
            if (ExpressionType.TryParse(r.StatusOperator, out tBinary))
            {
                var right = Expression.Constant(r.StatusValue);
                // use a binary operation, e.g. 'Equal' -> 'doc.Status == 1'
                var exp = Expression.MakeBinary(tBinary, left, right);
                
                return exp;
            }
            throw new Exception("Неизвестное выражение");
        }
    }

    public class EnabledRule
    {
        public string MemberName { get; set; }
        public string StatusName { get; set; }
        public string StatusOperator { get; set; }
        public int StatusValue { get; set; }

        public EnabledRule(string MemberName, string StatusName, string StatusOperator, int StatusValue)
        {
            this.MemberName = MemberName;
            this.StatusName = StatusName;
            this.StatusOperator = StatusOperator;
            this.StatusValue = StatusValue;
        }
    }

    
}

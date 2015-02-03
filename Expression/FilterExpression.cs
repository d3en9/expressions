using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressions
{
    public class FilterRule
    {
        public string MemberName { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }

        public FilterRule(string MemberName, string Operator, object Value)
        {
            this.MemberName = MemberName;
            this.Operator = Operator;
            this.Value = Value;
        }
    }

    class FilterExpression<T>
    {
        public List<T> Filter(IEnumerable<T> data, List<FilterRule> filter)
        {
            var compiledRules = filter.Select(r => CompileRule(r)).ToList();
            
            foreach (var rule in compiledRules)
            {
                data = data.Where(rule);
            }
            return data.ToList();
        }

        public Func<T, bool> CompileRule(FilterRule r)
        {
            var paramDoc = Expression.Parameter(typeof(T));
            Expression expr = BuildExpr(r, paramDoc);
            return Expression.Lambda<Func<T, bool>>(expr, paramDoc).Compile();
        }

        Expression BuildExpr(FilterRule r, ParameterExpression paramDoc)
        {
            var left = MemberExpression.Property(paramDoc, r.MemberName);
            ExpressionType tBinary;
            // is the operator a known .NET operator?
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                var right = Expression.Constant(r.Value);
                // use a binary operation, e.g. 'Equal' -> 'doc.Status == 1'
                var exp = Expression.MakeBinary(tBinary, left, right);

                return exp;
            }
            throw new Exception("Неизвестное выражение");
        }
    }
}

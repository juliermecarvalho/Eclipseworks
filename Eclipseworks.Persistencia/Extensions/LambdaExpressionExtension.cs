using System.Linq.Expressions;

namespace Eclipseworks.Persistencia.Extensions;

public static class LambdaExpressionExtension
{
    public static string AsPath(this LambdaExpression expression)
    {
        if (expression == null)
            return null;

        TryParsePath(expression.Body, out var path);

        return path;
    }

    private static bool TryParsePath(Expression expression, out string path)
    {
        var noConvertExp = RemoveConvertOperations(expression);
        path = null;

        switch (noConvertExp)
        {
            case MemberExpression memberExpression:
            {
                var currentPart = memberExpression.Member.Name;

                if (!TryParsePath(memberExpression.Expression, out var parentPart))
                    return false;

                path = string.IsNullOrEmpty(parentPart) ? currentPart : string.Concat(parentPart, ".", currentPart);
                break;
            }

            case MethodCallExpression callExpression:
                switch (callExpression.Method.Name)
                {
                    case nameof(Queryable.Select) when callExpression.Arguments.Count == 2:
                    {
                        if (!TryParsePath(callExpression.Arguments[0], out var parentPart))
                            return false;

                        if (string.IsNullOrEmpty(parentPart))
                            return false;

                        if (!(callExpression.Arguments[1] is LambdaExpression subExpression))
                            return false;

                        if (!TryParsePath(subExpression.Body, out var currentPart))
                            return false;

                        if (string.IsNullOrEmpty(parentPart))
                            return false;

                        path = string.Concat(parentPart, ".", currentPart);
                        return true;
                    }

                    case nameof(Queryable.Where):
                        throw new NotSupportedException("Filtering an Include expression is not supported");
                    case nameof(Queryable.OrderBy):
                    case nameof(Queryable.OrderByDescending):
                        throw new NotSupportedException("Ordering an Include expression is not supported");
                    default:
                        return false;
                }
        }

        return true;
    }
    public static Func<IQueryable<T>, IOrderedQueryable<T>> ThenBy<T>(
        this Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, object>> thenBy)
    {
        return aplicativos => orderBy(aplicativos).ThenBy(thenBy);
    }

    public static Func<IQueryable<T>, IOrderedQueryable<T>> ThenByDescending<T>(
        this Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, object>> thenByDescending)
    {
        return aplicativos => orderBy(aplicativos).ThenByDescending(thenByDescending);
    }

    private static Expression RemoveConvertOperations(Expression expression)
    {
        while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
            expression = ((UnaryExpression)expression).Operand;

        return expression;
    }
}
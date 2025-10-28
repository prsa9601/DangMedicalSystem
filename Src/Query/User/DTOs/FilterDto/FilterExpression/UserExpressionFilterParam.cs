using Common.Query.Filter;
using System.Linq.Expressions;

namespace Query.User.DTOs.FilterDto.FilterExpression
{
    public class UserExpressionFilterParam : BaseFilterParam
    {
        public Expression<Func<UserExpressionFilterData, bool>> Expression { get; set; }
    }
}

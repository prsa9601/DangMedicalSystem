using Common.Query.Filter;

namespace Query.User.DTOs.FilterDto
{
    public class UserFilterResult : BaseFilter<UserFilterData, UserFilterParam>
    {
    }
    public class UserFilterForDocumentsResult : BaseFilter<UserFilterForDocumentsData,
    UserFilterForDocumentsParam>
    {
    }
}

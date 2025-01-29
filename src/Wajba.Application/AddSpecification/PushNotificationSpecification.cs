using Wajba.AddSpecification;

namespace Fos_EF.AddSpecification
{
    public class PushNotificationSpecification : BaseSpecification<PushNotification>
    {
        public PushNotificationSpecification(
            string? title,
           DateTime? Date,

            int? roleId,
            int? userId,
            int pageNumber,
            int pageSize)
        {
            // Filter by Title if provided
            if (!string.IsNullOrEmpty(title))
            {
                AddCriteria(p => p.Title.Contains(title));
            }

            if (Date.HasValue)
            {
                AddCriteria(p => p.Date == Date.Value);
            }


            // Filter by RoleId if provided
            //if (roleId.HasValue)
            //{
            //    AddCriteria(p => p.rol == roleId.Value);
            //}

            //// Filter by UserId if provided
            //if (userId.HasValue)
            //{
            //    AddCriteria(p => p.WajbaUserId == userId.Value);
            //}

            //// Include related entities
            //AddInclude(p => p.User);
            //AddInclude(p => p.Rolle);

            // Apply pagination
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}

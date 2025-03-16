namespace Apprendi.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public virtual DateTimeOffset? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}

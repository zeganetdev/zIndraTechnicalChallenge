namespace zIndraTechnicalChallenge.Domain.MainContext.SeedWork
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime RegisterDate { get; set; }
        DateTime UpdateDate { get; set; }
        Guid RegisterUserId { get; set; }
        Guid UpdateUserId { get; set; }
    }

    public class Entity : IEntity
    {
        private int? _requestedHashCode;
        private Guid _Id;
        public virtual Guid Id { get => _Id; protected set => _Id = value; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual Guid RegisterUserId { get; set; }
        public virtual Guid UpdateUserId { get; set; }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Entity)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity item = (Entity)obj;
            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return (Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }

}

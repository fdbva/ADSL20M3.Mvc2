namespace Domain.Model.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
    }
}

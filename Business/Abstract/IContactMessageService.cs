using Core.BaseRequestModels;
using Business.ServiceBase;
using Core.Utils.Datatable;
using Model.Entities;
using Model.Dtos.ContactMessage_;

namespace Business.Abstract
{
    public interface IContactMessageService : IServiceBase<ContactMessage>, IServiceBaseAsync<ContactMessage>
    {
        Task<ContactMessage> CreateAsync(ContactMessageCreateDto contactMessage, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DatatableResponseServerSide<ContactMessage>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default);
    }
}
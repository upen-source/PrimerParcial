using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace SharedLib.Lodging
{
    [ServiceContract]
    public interface ILodgingController
    {
        [OperationContract]
        ValueTask<IEnumerable<LodgingReply>> GetAllLodging(CancellationToken cancellation);

        [OperationContract]
        ValueTask AddLodging(LodgingRequest lodging, CancellationToken cancellation);

        [OperationContract]
        ValueTask DeleteById(int id, CancellationToken cancellationToken);
    }
}

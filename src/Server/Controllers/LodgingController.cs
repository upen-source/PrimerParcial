using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Entities.Factories;
using Logic;
using Mapster;
using SharedLib.Lodging;

namespace Server.Controllers
{
    public class LodgingController : ILodgingController
    {
        private readonly LodgingService _lodgingService;

        public LodgingController(LodgingService lodgingService)
        {
            _lodgingService = lodgingService;
        }

        public async ValueTask<IEnumerable<LodgingReply>> GetAllLodging(
            CancellationToken cancellation)
        {
            return (await _lodgingService.GetAllLodging(cancellation))
                .Adapt<List<LodgingReply>>();
        }

        public async ValueTask AddLodging(LodgingRequest lodging, CancellationToken cancellation)
        {
            await _lodgingService.AddLodging(MapRequest(lodging), cancellation);
        }

        public async ValueTask DeleteById(DeleteRequest deleteRequest, CancellationToken cancellationToken)
        {
            await _lodgingService.DeleteById(deleteRequest.Id, cancellationToken);
        }

        private static Lodging MapRequest(LodgingRequest lodgingRequest)
        {
            Lodging lodging = LodgingFactory.CreateLodging(lodgingRequest.Type);
            lodging.PeopleAmount = lodgingRequest.PeopleAmount;
            lodging.EntryDate    = lodgingRequest.EntryDate;
            lodging.ExitDate     = lodgingRequest.ExitDate;
            lodging.RoomCapacity = MapRoomCapacity(lodgingRequest.RoomCapacity);
            return lodging;
        }

        private static RoomCapacity MapRoomCapacity(string capacity) =>
            capacity.ToLower(CultureInfo.CurrentCulture) switch
            {
                "familiar" => RoomCapacity.Familiar,
                "doble" => RoomCapacity.Doubly,
                "sencilla" => RoomCapacity.Simple,
                _ => RoomCapacity.Suite
            };
    }
}

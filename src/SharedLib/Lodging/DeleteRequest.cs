using System.Runtime.Serialization;

namespace SharedLib.Lodging
{
    [DataContract]
    public class DeleteRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        public DeleteRequest(int id)
        {
            Id = id;
        }

        public DeleteRequest()
        {
        }
    }
}

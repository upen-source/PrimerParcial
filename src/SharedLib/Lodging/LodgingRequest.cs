using System;
using System.Runtime.Serialization;

namespace SharedLib.Lodging
{
    [DataContract]
    public class LodgingRequest
    {
        [DataMember(Order = 1)]
        public string Type { get; set; }

        [DataMember(Order = 2)]
        public int PeopleAmount { get; set; }

        [DataMember(Order = 3)]
        public DateTime EntryDate { get; set; }

        [DataMember(Order = 4)]
        public DateTime ExitDate { get; set; }

        [DataMember(Order = 5)]
        public string RoomCapacity { get; set; }
    }
}

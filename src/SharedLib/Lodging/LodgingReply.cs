using System;
using System.Runtime.Serialization;

namespace SharedLib.Lodging
{
    [DataContract]
    public class LodgingReply
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int PeopleAmount { get; set; }

        [DataMember(Order = 3)]
        public DateTime EntryDate { get; set; }

        [DataMember(Order = 4)]
        public DateTime ExitDate { get; set; }

        [DataMember(Order = 5)]
        public string RoomCapacity { get; set; }

        [DataMember(Order = 6)]
        public int StayDays { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Room: {RoomCapacity}";
        }
    }
}

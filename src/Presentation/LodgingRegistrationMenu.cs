using System;
using System.Globalization;
using System.Linq;
using Entities;
using Presentation.Exceptions;
using Presentation.Filters;
using Presentation.UIBuilder;
using Presentation.Utils;
using SharedLib.Lodging;

namespace Presentation
{
    public class LodgingRegistrationMenu
    {
        private readonly BoxBuilder _boxBuilder;

        private readonly string[] _validGuestTypes = { "particular", "miembro", "premium" };

        public LodgingRequest CreateLodgingFromInput()
        {
            string       type         = AskGuestType();
            RoomCapacity roomCapacity = AskRoomCapacity();

            var lodging = new LodgingRequest
            {
                Type         = type,
                RoomCapacity = roomCapacity.AsString(),
                PeopleAmount = AskPeopleAmount(roomCapacity),
                EntryDate    = AskDate("Fecha de ingreso: "),
                ExitDate     = AskDate("Fecha de salida: ")
            };
            return lodging;
        }

        public LodgingRegistrationMenu(BoxBuilder boxBuilder)
        {
            _boxBuilder = boxBuilder;
        }

        [RepeatOnException]
        private string AskGuestType()
        {
            string validGuestOptions = string.Join('/', _validGuestTypes);
            Console.Write($"Tipo de huesped ({validGuestOptions}): ");
            string guestType = Console.ReadLine()?.ToLower(CultureInfo.InvariantCulture);
            return OnlyIfValid(guestType);
        }

        private string OnlyIfValid(string guestType)
        {
            if (!_validGuestTypes.Contains(guestType))
                throw new InvalidUserActionException("Opción inválida");
            return guestType;
        }

        private RoomCapacity AskRoomCapacity()
        {
            Menu           menu       = CreateRoomCapacityMenu();
            int            choice     = menu.DisplayAndRead();
            RoomCapacity[] capacities = GetAvailableCapacities();

            return capacities[choice - 1];
        }

        private Menu CreateRoomCapacityMenu()
        {
            string[] options = { "Familiar", "Sencilla", "Doble", "Suite", "", "" };

            return new MenuBuilder(_boxBuilder)
                .WithTitle("Tipo de habitación")
                .WithOptions(options)
                .WithQuestion("Ingrese una opcion: ")
                .Build();
        }

        private static RoomCapacity[] GetAvailableCapacities()
        {
            return Enum.GetValues(typeof(RoomCapacity))
                .Cast<RoomCapacity>()
                .ToArray();
        }

        private static int AskPeopleAmount(RoomCapacity roomCapacity)
        {
            int max   = roomCapacity.MaxCapacity();
            var range = new ARange(1, max);
            int peopleAmount = ConsoleReader.ReadFormattedData(
                $"Cantidad de huespedes (max {max}): ",
                Convert.ToInt32, range);
            return peopleAmount;
        }

        private static DateTime AskDate(string question)
        {
            return ConsoleReader.ReadFormattedData(question, Convert.ToDateTime);
        }
    }
}

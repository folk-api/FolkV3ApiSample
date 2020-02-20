using System;
using System.Linq;

namespace Us.FolkV3.ApiSample
{
    using Api.Client;
    using Api.Model;
    using Api.Model.Param;

    class Sample
    {
        private readonly String consumerHost;
        private readonly String consumerName;
        private PersonSmallClient smallClient;
        private PersonMediumClient mediumClient;
        private PrivateCommunityClient privateCommunityClient;
        private PublicCommunityClient publicCommunityClient;

        public Sample(string consumerHost, string consumerName)
        {
            this.consumerHost = consumerHost;
            this.consumerName = consumerName;
        }

        static void Main(string[] args)
        {
            // var sample = new Sample("{Heldin consumer security server name or IP}", "{Heldin consumer name}");
            // Call test methods applicable for your consumer.
        }

        private void Call(Action method)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine();
            }
        }

        private void TestSmallMethods()
        {
            Call(TestGetPersonSmallByPrivateId);
            Call(TestGetPersonSmallByPtal);
            Call(TestGetPersonSmallByNameAndAddress);
            Call(TestGetPersonSmallByNameAndDateOfBirth);
        }

        private void TestMediumMethods()
        {
            Call(TestGetPersonMediumByPrivateId);
            Call(TestGetPersonMediumByPublicId);
            Call(TestGetPersonMediumByPtal);
            Call(TestGetPersonMediumByNameAndAddress);
            Call(TestGetPersonMediumByNameAndDateOfBirth);
        }

        private void TestPrivateCommunityMethods()
        {
            Call(TestGetPrivateChanges);
            Call(TestAddPersonToCommunityByNameAndAddress);
            Call(TestAddPersonToCommunityByNameAndDateOfBirth);
            Call(TestRemovePersonFromCommunity);
            Call(TestRemovePersonsFromCommunity);
        }

        private void TestPublicCommunityMethods()
        {
            Call(TestGetPublicChanges);
        }

        private PersonSmallClient SmallClient()
        {
            if (smallClient == null)
            {
                smallClient = FolkClient.PersonSmall(consumerHost, consumerName);
            }
            return smallClient;
        }

        private PersonMediumClient MediumClient()
        {
            if (mediumClient == null)
            {
                mediumClient = FolkClient.PersonMedium(consumerHost, consumerName);
            }
            return mediumClient;
        }

        private PrivateCommunityClient PrivateCommunityClient()
        {
            if (privateCommunityClient == null)
            {
                privateCommunityClient = FolkClient.PrivateCommunity(consumerHost, consumerName);
            }
            return privateCommunityClient;
        }

        private PublicCommunityClient PublicCommunityClient()
        {
            if (publicCommunityClient == null)
            {
                publicCommunityClient = FolkClient.PublicCommunity(consumerHost, consumerName);
            }
            return publicCommunityClient;
        }


        // Test private methods

        private void TestGetPersonSmallByPrivateId()
        {
            Console.WriteLine("# TestGetPersonSmallByPrivateId");
            var person = SmallClient().GetPerson(
                    PrivateId.Create(1)
                    );
            PrintPerson(person);
        }

        private void TestGetPersonSmallByPtal()
        {
            Console.WriteLine("# TestGetPersonSmallByPtal");
            var person = SmallClient().GetPerson(
                    Ptal.Create("300408559")
                    );
            PrintPerson(person);
        }

        private void TestGetPersonSmallByNameAndAddress()
        {
            Console.WriteLine("# TestGetPersonSmallByNameAndAddress");
            var person = SmallClient().GetPerson(
                    NameParam.Create("Karius", "Davidsen"),
                    AddressParam.Create("Úti í Bø",
                            HouseNumber.Create(16),
                            "Syðrugøta")
                    );
            PrintPerson(person);
        }

        private void TestGetPersonSmallByNameAndDateOfBirth()
        {
            Console.WriteLine("# TestGetPersonSmallByNameAndDateOfBirth");
            var person = SmallClient().GetPerson(
                    NameParam.Create("Karius", "Davidsen"),
                    new DateTime(2008, 4, 30)
                    );
            PrintPerson(person);
        }


        // Test public methods

        private void TestGetPersonMediumByPrivateId()
        {
            Console.WriteLine("# TestGetPersonMediumByPrivateId");
            var person = MediumClient().GetPerson(
                    PrivateId.Create(1)
                    );
            PrintPerson(person);
        }

        private void TestGetPersonMediumByPublicId()
        {
            Console.WriteLine("# TestGetPersonMediumByPublicId");
            var person = MediumClient().GetPerson(
                    PublicId.Create(1157442)
                    );
            PrintPerson(person);
        }

        private void TestGetPersonMediumByPtal()
        {
            Console.WriteLine("# TestGetPersonMediumByPtal");
            var person = MediumClient().GetPerson(
                    Ptal.Create("300408559")
                    );
            PrintPerson(person);
        }

        private void TestGetPersonMediumByNameAndAddress()
        {
            Console.WriteLine("# TestGetPersonMediumByNameAndAddress");
            var person = MediumClient().GetPerson(
                    NameParam.Create("Karius", "Davidsen"),
                    AddressParam.Create("Úti í Bø",
                            HouseNumber.Create(16),
                            "Syðrugøta")
                    );
            PrintPerson(person);
        }

        private void TestGetPersonMediumByNameAndDateOfBirth()
        {
            Console.WriteLine("# TestGetPersonMediumByNameAndDateOfBirth");
            var person = MediumClient().GetPerson(
                    NameParam.Create("Karius", "Davidsen"),
                    new DateTime(2008, 4, 30)
                    );
            PrintPerson(person);
        }


        // Test community methods

        private void TestGetPrivateChanges()
        {
            Console.WriteLine("# TestGetPrivateChanges");
            Changes<PrivateId> changes = PrivateCommunityClient().GetChanges(DateTime.Now.AddDays(-7));
            Console.WriteLine("Changes - from: {0}; to: {1}; ids: [{2}]\n", changes.From, changes.To, string.Join(", ", changes.Ids));
        }

        private void TestGetPublicChanges()
        {
            Console.WriteLine("# TestGetPublicChanges");
            Changes<PublicId> changes = PublicCommunityClient().GetChanges(DateTime.Now.AddDays(-7));
            Console.WriteLine("Changes - from: {0}; to: {1}; ids: [{2}]\n", changes.From, changes.To, string.Join(", ", changes.Ids));
        }

        private void TestAddPersonToCommunityByNameAndAddress()
        {
            Console.WriteLine("# TestAddPersonToCommunityByNameAndAddress");
            var communityPerson = PrivateCommunityClient().AddPersonToCommunity(
                    NameParam.Create("Karius", "Davidsen"),
                    AddressParam.Create("Úti í Bø",
                            HouseNumber.Create(16),
                            "Syðrugøta")
                    );
            PrintCommunityPerson(communityPerson);
        }

        private void TestAddPersonToCommunityByNameAndDateOfBirth()
        {
            Console.WriteLine("# TestAddPersonToCommunityByNameAndDateOfBirth");
            var communityPerson = PrivateCommunityClient().AddPersonToCommunity(
                    NameParam.Create("Karius", "Davidsen"),
                    new DateTime(2008, 4, 30)
                    );
            PrintCommunityPerson(communityPerson);
        }

        private void TestRemovePersonFromCommunity()
        {
            Console.WriteLine("# TestRemovePersonFromCommunity");
            var removedId = PrivateCommunityClient().RemovePersonFromCommunity(PrivateId.Create(1));
            Console.WriteLine("Removed id: {0}\n", removedId);
        }

        private void TestRemovePersonsFromCommunity()
        {
            Console.WriteLine("# TestRemovePersonsFromCommunity");
            var removedIds = PrivateCommunityClient().RemovePersonsFromCommunity(PrivateId.Create(1, 2, 3));
            Console.WriteLine("Removed ids: [{0}]\n", string.Join(", ", removedIds));
        }


        // Print methods

        private static void PrintPerson(PersonSmall person)
        {
            if (person == null)
            {
                Console.WriteLine("Person was not found!");
            }
            else
            {
                Console.WriteLine(PersonToString(person));
            }
            Console.WriteLine();
        }

        private static void PrintCommunityPerson(CommunityPerson person)
        {
            if (person == null)
            {
                Console.WriteLine("Oops!");
            }
            else
            {
                Console.WriteLine(CommunityPersonToString(person));
            }
            Console.WriteLine();
        }

        private static String PersonToString(PersonSmall person)
        {
            if (person.GetType() == typeof(PersonMedium)) {
                var personPublic = (PersonMedium)person;
                return Format(person.PrivateId, personPublic.PublicId, person.Name, AddressToString(person),
                        personPublic.DateOfBirth,
                        CivilStatusToString(personPublic), SpecialMarksToString(personPublic),
                        IncapacityToString(personPublic));
            }
            var deadOrAlive = person.IsAlive ? "ALIVE" : ("DEAD " + person.DateOfDeath);
            return Format(person.PrivateId, person.Name, AddressToString(person), deadOrAlive);
        }

        private static String CommunityPersonToString(CommunityPerson communityPerson)
        {
            String personString = null;
            if (communityPerson.IsAdded)
            {
                personString = PersonToString(communityPerson.Person);
            }
            return Format(communityPerson.Status, communityPerson.ExistingId, personString);
        }

        private static String AddressToString(PersonSmall person)
        {
            return AddressToString(person.Address);
        }

        private static String AddressToString(Address address)
        {
            return address.HasStreetAndNumbers
                            ? address.StreetAndNumbers
                                    + "; " + address.Country.Code + address.PostalCode
                                    + " " + address.City
                                    + "; " + address.Country.NameFo
                                    + " (From: " + address.From + ')'
                            : null;
        }

        private static String CivilStatusToString(PersonMedium person)
        {
            if (person.CivilStatus == null)
            {
                return null;
            }
            return person.CivilStatus.Type + ", " + person.CivilStatus.From;
        }

        private static String SpecialMarksToString(PersonMedium person)
        {
            return person.SpecialMarks.IsEmpty
                    ? null : ('[' + string.Join(", ", person.SpecialMarks.Select(s => s.ToString())) + ']');
        }

        private static String IncapacityToString(PersonMedium person)
        {
            if (person.Incapacity == null)
            {
                return null;
            }
            var guardian1 = GuardianToString(person.Incapacity.Guardian1);
            var guardian2 = GuardianToString(person.Incapacity.Guardian2);
            return guardian2 == null ? guardian1 : guardian1 + " / " + guardian2;
        }

        private static String GuardianToString(Guardian guardian)
        {
            if (guardian == null)
            {
                return null;
            }
            return guardian.Name + " - " + AddressToString(guardian.Address);
        }

        private static String Format(params Object[] values)
        {
            return string.Join(
                " | ",
                values.ToList().Select(v => v == null ? "-" : v.ToString())
                );
        }

    }
}

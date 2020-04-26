using Microsoft.DSX.ProjectTemplate.Data.Models;
using System;
using System.Linq;

namespace Microsoft.DSX.ProjectTemplate.Data.Utilities
{
    public static class RandomFactory
    {
        private static readonly Random _random = new Random();
        private static readonly object _randLock = new object();

        // https://en.wikipedia.org/wiki/List_of_fictional_Microsoft_companies
        private static readonly string[] _companyNames =
        {
            "A. Datum Corporation",
            "AdventureWorks Cycles",
            "Alpine Ski House",
            "Awesome Computers",
            "Baldwin Museum of Science",
            "Blue Yonder Airlines",
            "City Power & Light",
            "Coho Vineyard & Winery",
            "Consolidated Messenger",
            "Contoso Ltd.",
            "cpandl.com",
            "CRONUS",
            "Electronic, Inc.",
            "Fabrikam, Inc.",
            "Fourth Coffee",
            "FusionTomo",
            "Graphic Design Institute",
            "Humongous Insurance",
            "ItExamWorld.com",
            "LitWare Inc.",
            "Lucerne Publishing",
            "Margie's Travel",
            "Northridge Video",
            "Northwind Traders",
            "Olympia",
            "Parnell Aerospace",
            "ProseWare, Inc.",
            "School of Fine Art",
            "Southbridge Video",
            "TailSpin Toys",
            "Tasmanian Traders",
            "The Phone Company",
            "Trey Research Inc.",
            "The Volcano Coffee Company",
            "WingTip Toys",
            "Wide World Importers",
            "Woodgrove Bank"
        };

        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly string[] _libraryNames = new string[]
        {
            "Forest Grove Library",
            "Ocean View Library",
            "Mountain Cliff Library",
            "Bird Wing Library",
            "Yellow Jacket Library",
            "Elgin Library",
            "Green Forest Library",
            "Angler Way Library",
            "Rainbow Trout Library",
            "Washington Road Library",
            "Black Cherry Library",
            "Green Apple Road Library",
            "Salty Spoon Library",
            "Long Road Library",
            "Desert Sun Library"
        };

        private static readonly string[] _locationAddress1 = new string[]
        {
            "1242 Maple Dr SE",
            "1141 Forrest Drive N",
            "2130 Golf Way S",
            "982 Bowling Lane Ave",
            "9759 Red Ridge Ave NW",
            "9501 Ocean Way",
            "4958 Cliff Side Blvd"
        };

        private static readonly string[] _locationCity = new string[]
        {
            "Cloverhill",
            "Bellevue",
            "Sammammish",
            "Issaquah",
            "Algonquin",
            "Lake in the Hills",
            "Crystal Lake",
            "Everett",
            "Lynnwood",
            "Schamburg"
        };

        private static readonly string[] _states = new string[]
        {
            "AL",
            "AK",
            "AS",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "DC",
            "FL",
            "GA",
            "GU",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MH",
            "MA",
            "MI",
            "FM",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "VI",
            "WA",
            "WV",
            "WI",
            "WY"
        };

        private static readonly string[] _zipCodes = new string[]
        {
            "94859",
            "94718",
            "19959",
            "94727",
            "29581",
            "39591",
            "30591",
            "95001",
            "30301",
            "39501",
            "39301",
            "39359",
            "90159"
        };
        
        private static readonly string[] _codeNames = new string[]
            {
                "Algiers",
                "Amazon",
                "Amsterdam",
                "Annapurna",
                "Aphrodite",
                "Apollo",
                "Ares",
                "Artemis",
                "Athens",
                "Baltis",
                "Berlin",
                "Bogota",
                "Calabar",
                "Casablanca",
                "Caspian",
                "Centaurus",
                "Ceres",
                "Demeter",
                "Dresden",
                "Erie",
                "Eris",
                "Everest",
                "Flora",
                "Geneva",
                "Giza",
                "Hades",
                "Halifax",
                "Helsinki",
                "Hestia",
                "Huron",
                "Jakar",
                "Janus",
                "Juno",
                "Jupiter",
                "K2",
                "Kathmandu",
                "Keflavik",
                "Kingston",
                "Kyoto",
                "Ladoga",
                "Luxor",
                "Malawi",
                "Manila",
                "Maribor",
                "Mars",
                "Melbourne",
                "Mercury",
                "Minerva",
                "Mississippi",
                "Nazret",
                "Neptune",
                "Nile",
                "Orcus",
                "Perth",
                "Pomona",
                "Poseidon",
                "Ridder",
                "Rift",
                "Riga",
                "Saimaa",
                "Sarajevo",
                "Sarband",
                "Saturn",
                "Seine",
                "Sol",
                "Sparta",
                "Strand",
                "Tallinn",
                "Tellus",
                "Themes",
                "Toledo",
                "Trevi",
                "Turku",
                "Venus",
                "Vesta",
                "Vilnius",
                "Visby",
                "Vulcan",
                "Westeros",
                "Zeus"
            };

        public static int GetInteger(int inclusiveMin, int exclusiveMax)
        {
            lock (_randLock)
            {
                return _random.Next(inclusiveMin, exclusiveMax);
            }
        }

        public static string GetAlphanumericString(int length)
        {
            lock (_randLock)
            {
                return new string(Enumerable.Repeat(_chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
            }
        }

        public static bool GetBoolean()
        {
            lock (_randLock)
            {
                return _random.Next(0, 2) == 1;
            }
        }

        public static string GetCodeName()
        {
            lock (_randLock)
            {
                return _codeNames[_random.Next(_codeNames.Length)];
            }
        }

        public static string GetCompanyName()
        {
            lock (_randLock)
            {
                return _companyNames[_random.Next(0, _companyNames.Length)];
            }
        }

        // Retrieves a new random library name
        public static string GetLibraryName()
        {
            lock (_randLock)
            {
                return _libraryNames[_random.Next(_libraryNames.Length)];
            }
        }

        // Retrieves a random street address
        public static string GetStreetAddress()
        {
            lock (_randLock)
            {
                return _locationAddress1[_random.Next(_locationAddress1.Length)];
            }
        }

        // Retrieves a random city
        public static string GetCity()
        {
            lock (_randLock)
            {
                return _locationCity[_random.Next(_locationCity.Length)];
            }
        }

        // Retrieves a random state
        public static string GetState()
        {
            lock (_randLock)
            {
                return _states[_random.Next(_states.Length)];
            }
        }

        // Retrieves a random zip code
        public static string GetZip()
        {
            lock (_randLock)
            {
                return _zipCodes[_random.Next(_zipCodes.Length)];
            }
        }
    }
}

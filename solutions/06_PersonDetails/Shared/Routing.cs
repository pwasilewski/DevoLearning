namespace Nihdi.DevoLearning.Presentation.Shared
{
    public static class Routing
    {
        public static class Home
        {
            public const string Index = "/";
        }

        public static class Persons
        {
            public const string Overview = "/Persons";
            public const string PersonDetails = "/Persons/{id:int}/Details";
        }
    }
}
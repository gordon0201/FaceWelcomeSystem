namespace FaceWelcome.API.Constants
{
    public static class APIEndPointConstant
    {
        private const string RootEndPoint = "/api";
        private const string ApiVersion = "/v1";
        private const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Person
        {
            public const string PersonsEndpoint = ApiEndpoint + "/persons";
            public const string PersonEndpoint = PersonsEndpoint + "/{id}";
        }
        public static class Event
        {
            public const string EventsEndpoint = ApiEndpoint + "/events";  // Đảm bảo đường dẫn chính xác
            public const string EventEndpoint = EventsEndpoint + "/{id}";
            public const string ListGuestsEndpoint = EventEndpoint + "guests";
        }

        public static class Organization
        {
            public const string OrganizationsEndpoint = ApiEndpoint + "/organizations";
            public const string OrganizationEndpoint = OrganizationsEndpoint + "/{code}";
        }

        public static class OrganizationGroup
        {
            public const string OrganizationGroupsEndpoint = ApiEndpoint + "/organizationGroups";  // Đảm bảo đường dẫn chính xác
            public const string OrganizationGroupEndpoint = OrganizationGroupsEndpoint + "/{id}";
        }

        public static class Guest
        {
            public const string GuestsEndpoint = ApiEndpoint + "/guests";  // Đảm bảo đường dẫn chính xác
            public const string GuestEndpoint = GuestsEndpoint + "/{id}";
        }


        public static class Staff
        {
            public const string StaffsEndpoint = ApiEndpoint + "/staffs";
            public const string StaffEndpoint = StaffsEndpoint + "/{id}";
        }

        public static class WelcomeTemplate
        {
            public const string welcomeTemplatesEndpoint = ApiEndpoint + "/welcomeTemplates";
            public const string welcomeTemplateEndpoint = welcomeTemplatesEndpoint + "/{id}";

        }
        public static class GuestImage
        {
            public const string GuestImagesEndpoint = ApiEndpoint + "/guestImages";
            public const string GuestImageEndpoint = GuestImagesEndpoint + "/{id}";
        }

        public static class Group
        {
            public const string GroupsEndpoint = ApiEndpoint + "/groups";
            public const string GroupEndpoint = GroupsEndpoint + "/{id}";

        }
    }
}


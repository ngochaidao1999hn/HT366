namespace HT366.Domain.Common.Enums
{
    public enum LevelEnum
    {
        GENERAL,
        LOP_1,
        LOP_2,
        LOP_3,
        LOP_4,
        LOP_5,
        LOP_6,
        LOP_7,
        LOP_8,
        LOP_9,
        LOP_10,
        LOP_11,
        LOP_12
    }

    public enum StatusEnum
    {
        Inactive,
        Active,
        Deleted,
        Pending
    }

    public enum SexEnum
    {
        Male,
        Female,
        Other
    }

    public static class Roles
    {
        public static readonly string Admin = "Admin";
        public static readonly string Student = "Student";
        public static readonly string Teacher = "Teacher";
    }
}
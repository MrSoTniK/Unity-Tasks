public static  class ConstantsContainer
{
    public static class Expression 
    {           
        public static class Visualization 
        {
            public const string Addition = "x + ";
            public const string Subtraction = "x - ";
            public const string Multiplication = "x * ";
            public const string Division = "x / ";
        }

        public static class Good 
        {
            public const int Addition = 1;
            public const int Multiplication = 2;
            public const int Division = 3;
        }

        public static class Bad
        {
            public const int Subtraction = -1;
            public const int Multiplication = -2;
            public const int Division = -3;
        }

        public static class EasyMode
        {
            public const int MinGoodID = 1;
            public const int MaxGoodID = 3;
            public const int MinBadID = -3;
            public const int MaxBadID = -1;
        }

        public static class HardMode
        {

        }
    }
}
namespace PlayMeowDemo
{ 
    public static class CommonDefine
    {
        public const int PwMinLen           = 6;
        public const float ErrorShowTime    = 3.5f;
    }

    public static class UIRequest
    {
        // login
        public static string Login              = "Login";
        public static string GoogleLogin        = "GoogleLogin";
        public static string RegisterNewAcc     = "RegisterNewAcc";
        public static string ForwgetPassWord    = "ForwgetPassWord";
        public static string Close              = "Close";

        // T&C
        public static string ShowPrivacy        = "ShowPrivacy";
        public static string ShowTC             = "ShowTC";
    }

    public static class UICommand
    {
        public static string OpenLogin          = "OpenLogin";
        public static string ShowLoginError     = "ShowLoginError";
    }
}

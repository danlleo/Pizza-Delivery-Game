namespace Misc
{
    public static class InputAllowance
    {
        public static bool InputEnabled { get; private set; }

        public static void EnableInput()
        {
            InputEnabled = true;
        }

        public static void DisableInput()
        {
            InputEnabled = false;
        }
    }
}
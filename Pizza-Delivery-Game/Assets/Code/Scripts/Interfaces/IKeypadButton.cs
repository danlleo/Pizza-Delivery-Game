using Enums.Keypad;

namespace Interfaces
{
    public interface IKeypadButton
    {
        public void Press();
        public void SetType(ButtonType buttonType);
    }
}
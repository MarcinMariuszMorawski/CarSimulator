using System.Drawing;

namespace CarSimulatorUserInterfaceConsole.Model
{
    public class BasicBox
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }

        public BasicBox(string title, string value)
        {
            Title = title;
            Value = value;
            BackgroundColor = Color.Empty;
            TextColor = Color.Empty;
        }

        public BasicBox(string title, string value, Color backgroundColor, Color textColor)
        {
            Title = title;
            Value = value;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
}
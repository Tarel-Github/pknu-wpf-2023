using System.Windows.Media;

namespace wp05_bikeshop.Logics
{
    internal class Human
    {
        public string FullName { get; set; }
        public bool HasLicense { get; set; }
        public Color Colors { get; set; }
        public Human Driver { get; set; }
    }
}

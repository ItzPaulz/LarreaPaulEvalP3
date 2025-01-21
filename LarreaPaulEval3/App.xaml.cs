using System.IO;


namespace LarreaPaulEval3
{
    public partial class App : Application
    {
        public static string DatabasePath { get; private set; }

        public App(string databasePath)
        {
            InitializeComponent();

            DatabasePath = databasePath;

            MainPage = new AppShell(); 
        }
    }
}
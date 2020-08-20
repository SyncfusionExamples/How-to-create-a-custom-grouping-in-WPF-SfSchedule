using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SfSchedule_CustomGrouping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DateTime> datecoll = new ObservableCollection<DateTime>();
        private ObservableCollection<DateTime> dateselected = new ObservableCollection<DateTime>();
        public ScheduleAppointmentCollection FilterColl { get; set; }
        Random random;
        public MainWindow()
        {
            this.InitializeComponent();

            television.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            random = new Random();
            for (int i = -20; i < 20; i += 2)
            {
                for (int j = -7; j < 7; j++)
                {
                    datecoll.Add(DateTime.Now.Date.AddDays(j).AddHours(i));
                }
            }
            this.FilterColl = GetFilterCollection();
            this.DataContext = this;
        }
        private ScheduleAppointmentCollection GetFilterCollection()
        {
            string[] football = new string[] { "USA vs IRN ", "RUS vs ARG", "POR vs SWI", "BEL vs ARG", "USA vs RUS", "IRN vs POR", "CRO vs MEX", "JPN vs ITA", "COL vs URU" };
            string[] cricket = new string[] { "IND vs PAk", "AUS vs SA", "SRI vs WI", "SA vs IND", "ZIM vs PAK", "SRI vs AUS" };
            string[] news = new string[] { "Cricket news", "Football news", "Hockey news", "Tennis news", "news of Athletics", "Golf news", "Kabaddi news", "VolleyBall news" };
            string[] pgms = new string[] { "FootBall", "Cricket", "News" };
            ScheduleAppointmentCollection app = new ScheduleAppointmentCollection();
            for (int dc = 0; dc < datecoll.Count - 1; dc++)
            {
                DateTime date = datecoll[dc];
                dateselected.Add(date);

                for (int sd = 0; sd < dateselected.Count - 1; sd++)
                {
                    if (date == dateselected[sd])
                    {
                        date = date.AddDays(1);
                    }
                }
                ScheduleAppointment app1 = new ScheduleAppointment() { StartTime = date };
                app1.ResourceCollection.Add(new Resource() { ResourceName = "TV" + random.Next(1, 4), TypeName = "TV" });
                app1.ResourceCollection.Add(new Resource() { ResourceName = pgms[random.Next(0, 3)], TypeName = "programs" });
                if ((app1.ResourceCollection[1] as Resource).ResourceName.Equals("FootBall"))
                {
                    app1.Subject = football[random.Next(0, football.Length)];
                    app1.EndTime = date.AddHours(1);
                }
                else if ((app1.ResourceCollection[1] as Resource).ResourceName.Equals("Cricket"))
                {
                    app1.Subject = cricket[random.Next(0, cricket.Length)];
                    app1.EndTime = date.AddHours(1);
                }
                else
                {
                    app1.Subject = news[random.Next(0, news.Length)];
                    app1.EndTime = date.AddHours(1);
                }
                app1.AppointmentBackground = new SolidColorBrush(Colors.DimGray);

                app.Add(app1);
            }
            return app;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string str = (sender as Button).Content.ToString();
            if (str.Equals("Television"))
            {
                programs.IsEnabled = true;
                this.schedule.Resource = "TV";
                television.IsEnabled = false;
            }
            else
            {
                television.IsEnabled = true;
                this.schedule.Resource = "programs";
                programs.IsEnabled = false;
            }
        }

    }
}

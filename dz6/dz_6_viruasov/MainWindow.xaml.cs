using System;
using System.Collections.Generic;
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

namespace dz_6_viruasov
{
    
    public partial class MainWindow : Window
    {
        public abstract class Transport
        {
            public string model;
            public int fuelConsuption;
            public int speed;
            public int passagers;

            public Transport(string mod, int fuel, int spd, int pas) {
                model = mod;
                fuelConsuption = fuel;
                speed = spd;
                passagers = pas;
            }
            public virtual double getFuelByDistance(int distance) {
                double result = (double)distance / 100 * (double)fuelConsuption;
                return result;
            }
            public  double getTimeByDistance(int distance)
            {
                double result = (double)distance / (double)speed;
                return result;
            }
        }
        public class Vel : Transport 
        {
            public Vel(string mod, int fuel, int speed, int pas) :
                base(mod, fuel, speed, pas)
            { }
            public override double getFuelByDistance(int distance) 
            {
                return 0;
            }
            public override string ToString()
            {
                return $"Велосипед: {model}\n\t Расход топлива: 0\n\t" +
                    $"Средняя скорость: {speed}\n\t" +
                    $"Количество пассажиров: {passagers}\n";
            }
        }
        public class auto : Transport
        {
            public auto(string mod, int fuel, int speed, int pas) :
                base(mod, fuel, speed, pas)
            { }

            public override string ToString()
            {
                return $"Автомобиль: {model}\n\t Расход топлива: {fuelConsuption}\n\t" +
                    $"Средняя скорость: {speed}\n\t" +
                    $"Количество пассажиров: {passagers}\n";
            }
        }
        public class Track : Transport
        {
            public Track(string mod, int fuel, int speed, int pas) :
                base(mod, fuel, speed, pas)
            { }

            public override string ToString()
            {
                return $"Грузовик: {model}\n\t Расход топлива: {fuelConsuption}\n\t" +
                    $"Средняя скорость: {speed}\n\t" +
                    $"Количество пассажиров: {passagers}\n";
            }
        }
        List<Transport> transports = new List<Transport>();
        public MainWindow()
        {
            InitializeComponent();
            transports.Add(new Vel("stels", 0, 15, 2));
            transports.Add(new auto("audi", 9, 120, 5));
            transports.Add(new Track("KAMAZ", 51, 60, 3));
            
            foreach (var tr in transports) {
                tb_result.Text += tr.ToString();
                cb_transports.Items.Add(tr);
            }
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int speed = int.Parse(tb_speed.Text);
                int pas = int.Parse(tb_pas.Text);
                string model = tb_model.Text;
                int fuel = int.Parse(tb_fuel.Text);

                int selector = cb_choice.SelectedIndex;
                switch (selector)
                {
                    case 0:
                        transports.Add(new Vel(model, fuel = 0, speed, pas));
                        tb_result.Text += transports[transports.Count - 1].ToString();
                        cb_transports.Items.Add(transports[transports.Count - 1]);
                        break;
                    case 1:
                        transports.Add(new auto(model, fuel, speed, pas));
                        tb_result.Text += transports[transports.Count - 1].ToString();
                        cb_transports.Items.Add(transports[transports.Count - 1]);
                        break;
                    case 2:
                        transports.Add(new Track(model, fuel, speed, pas));
                        tb_result.Text += transports[transports.Count - 1].ToString();
                        cb_transports.Items.Add(transports[transports.Count - 1]);
                        break;
                }
            }
            catch (System.FormatException)
            {

                tb_result.Text = "Введите корректные данные";
            }
        }
        
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            foreach (var tr in transports)
            {
                tb_result.Text += tr.ToString();
            }
        }

        private void btn_fuel_Click(object sender, RoutedEventArgs e)
        {
            int i = cb_transports.SelectedIndex;
            int distance = int.Parse(tb_distance.Text);
            string result = $"Потребуется топлива:{transports[i].getFuelByDistance(distance)}";
            tb_result.Text = result;
        }

        private void btn_fuel_Copy_Click(object sender, RoutedEventArgs e)
        {
            int i = cb_transports.SelectedIndex;
            int distance = int.Parse(tb_distance.Text);
            string result = $"Потребуется времени:{transports[i].getTimeByDistance(distance)}";
            tb_result.Text = result;
        }
    }
}

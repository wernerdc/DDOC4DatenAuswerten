
using System.Text;

namespace DDOC4DatenAuswerten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.WriteLine("DDOC 4 DatenAuswerten\n");

            // Double Array mit nMax Elementen anlegen 
            int nMax = 20;
            double[] data = new double[nMax];
            Value[] vData = new Value[nMax];

            // Array mit Testdaten initialisieren 
            ArrayInitialisieren(data);
            ArrayInitialisieren(vData);

            // Array auf Konsole ausgeben 
            Console.WriteLine("double[]");
            ArrayAusgeben(Console.Out, data);
            Console.WriteLine("Value[]");
            ArrayAusgeben(Console.Out, vData);

            // Daten auswerten und Ergebnis in Instanz von Typ Statistik speichern 
            Statistik stat = StatistikBerechnen(data);
            Statistik vStat = StatistikBerechnen(vData);

            // Auswertung auf die Konsole ausgeben 
            Console.WriteLine("\ndouble[]");
            StatistikAusgeben(Console.Out, stat);
            Console.WriteLine("Value[]");
            StatistikAusgeben(Console.Out, vStat);


            Console.WriteLine("\n\npress [ENTER] to exit...");
            Console.ReadLine();
        }

        private static void ArrayAusgeben(TextWriter writer, double[] data)
        {
            StringBuilder sb = new();
            foreach (double d in data)
            {
                sb.Append(d);
                sb.Append(' ');
            }
            writer.WriteLine(sb.ToString());
        }
        
        private static void ArrayAusgeben(TextWriter writer, Value[] data)
        {
            StringBuilder sb = new();
            foreach (var item in data)
            {
                sb.Append(item.Wert);
                sb.Append(' ');
            }
            writer.WriteLine(sb.ToString());
        }


        private static void StatistikAusgeben(TextWriter writer, Statistik stat)
        {
            writer.WriteLine(stat);
        }

        private static void ArrayInitialisieren(double[] data, int randomInt = 123)
        {
            Random r = new(randomInt);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Math.Round(r.Next(0, 1000) / 10.0, 1);
            }
        }
        private static void ArrayInitialisieren(Value[] data, int randomInt = 123)
        {
            Random r = new(randomInt);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Value(r.Next(0, 4),
                    Math.Round(r.Next(0, 1000) / 10.0, 1));
            }
        }

        private static Statistik StatistikBerechnen(double[] data)
        {
            double min = data[0];
            double max = data[0];
            double sum = 0;
            foreach (var item in data)
            {
                min = (item < min) ? item : min;
                max = (item > max) ? item : max;
                sum += item;
            }
            return new Statistik(
                data.Length, 
                min,
                max,
                (double)sum / data.Length);
        }
        
        private static Statistik StatistikBerechnen(Value[] data)
        {
            double[] dData = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                dData[i] = data[i].Wert;
            }
            return StatistikBerechnen(dData);
        }


    }

    public class Statistik
    {
        public Statistik(int anzahl, double minimum, double maximum, double mittelwert)
        {
            Datum = DateTime.Now;
            Anzahl = anzahl;
            Minimum = minimum;
            Maximum = maximum;
            Mittelwert = mittelwert;

        }

        public DateTime Datum { get; set; }
        public int Anzahl { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Mittelwert { get; set; }

        public override string ToString()
        {
            return $"Datum: {Datum.ToShortDateString(),-12}   " +
                   $"Anzahl: {Anzahl,-8:N0}   " +
                   $"Minimum: {Minimum,-10:N2}   " +
                   $"Maximum: {Maximum,-10:N2}   " +
                   $"Mittelwert: {Mittelwert,-10:N2}";
        }
    
    }

    public class Value
    {
        public Value(int sensorId, double wert)
        {
            SensorId = sensorId;
            Wert = wert;
            Timestamp = DateTime.Now;
        }

        public int SensorId { get; set; }
        public double Wert { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"SensorId: {SensorId,10} " +
                   $"Wert: {Wert,14:N2} " +
                   $"Timestamp: {Timestamp}";
        }
    }
}

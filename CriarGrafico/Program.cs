using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace CriarGrafico
{
    class Program
    {

        static void Main(string[] args)
        {
            Color[] cores = new Color[13] {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Coral, Color.DarkCyan,
                                Color.DeepPink, Color.Gold, Color.Gray, Color.Magenta, Color.Brown, Color.DarkViolet};

            
            for (int i = 3; i < 6; i++)
            {
                List<Ponto> list = LerArquivo("c2ds1-2spSL" + i + "Dados.txt");
                CriarGraficoSalvar(list, cores, "grafico" + i);
            }
            

            /*
            List<Ponto> list = LerArquivo("c2ds3-2gSL5Dados.txt");
            CriarGraficoSalvar(list, cores, "grafico");
            */

        }


        public static List<Ponto> LerArquivo(string nome)
        {

            List<Ponto> list = new List<Ponto>();

            StreamReader fsSource = new StreamReader(nome);

            while (fsSource.Peek() >= 0)
            {
                string line = fsSource.ReadLine();

                string[] words = line.Split(',');

                Ponto ponto = new Ponto(Double.Parse(words[2], CultureInfo.InvariantCulture), Double.Parse(words[3], CultureInfo.InvariantCulture), Int32.Parse(words[1]));

                list.Add(ponto);
            }

            fsSource.Close();

            return list;
        }

        public static void CriarGraficoSalvar(List<Ponto> values, Color[] cores, string filename)
        {
            Chart chart = new Chart();
            Series series = new Series();
            ChartArea chartArea1 = new ChartArea();

            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            chart.Size = new Size(1200, 800);

            series.BorderWidth = 2;
            series.BorderDashStyle = ChartDashStyle.Solid;
            series.ChartType = SeriesChartType.Point;
            //series.Color = Color.Green;

            double maior = 0;
            double menor = Double.PositiveInfinity;

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].getY() > maior)
                    maior = values[i].getY();
                if (values[i].getY() < menor)
                    menor = values[i].getY();


                DataPoint dP = new DataPoint(values[i].getX(), values[i].getY());
                series.Points.Insert(i, dP);

                series.Points[i].Color = cores[values[i].getCor() - 1];
                
            }

            chart.BorderlineColor = Color.Red;
            chart.BorderlineWidth = 1;
            chart.Series.Add(series);
            chart.Titles.Add("Grafico");
            chart.Palette = ChartColorPalette.Fire;
            chart.Invalidate();
            
            chartArea1.AxisY.Minimum = menor;
            chartArea1.AxisY.Maximum = maior;
            
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MinorGrid.Enabled = false;

            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MinorGrid.Enabled = false;


            chart.SaveImage(filename + ".bmp", ChartImageFormat.Bmp);

        }

    }
}

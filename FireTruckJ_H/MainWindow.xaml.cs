using System;
using System.Collections.Generic;
using System.IO;
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

namespace FireTruckJ_H
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<string> test = new List<string>();
        List<int> currentindex = new List<int>();
        public int maxn = 99999;//need to find another way to identify the infinity
        public int availableRoute;
        public int visitable;
        public int task = 1;
        public int[] vis = new int[22];
        public int[] recordRoute = new int[30];
        public int[,] panel = new int[22, 22];
        public int[,] floyd = new int[22, 22];
        public string startpoint = "1";
        public string route = "";
        public int firelocation;
        public string msg = "";
        public List<string> lines = new List<string>(File.ReadAllLines(@"input.txt"));

        public void dfs(int x, int n)//can use dfs or bfs the result should be the same
        {
            if (x == firelocation)
            {
                route = "";
                for (int i = 1; i < n - 1; i++)
                    route += recordRoute[i].ToString();
                msg += startpoint + route + firelocation + Environment.NewLine;
                availableRoute++;
                return;
            }
            for (int i = 1; i <= visitable; i++)
            {//floyd method here is used for making judgement, to get rid of unnecessary routes, and is part of pruning
                if (vis[i] == 0 && panel[x, i] == 1 && floyd[firelocation, i] != maxn)
                {
                    recordRoute[n] = i;
                    vis[i] = 1;
                    dfs(i, n + 1);
                    vis[i] = 0;
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            List<string> inputs = new List<string>(File.ReadAllLines(@"input.txt"));
            foreach (var input in inputs)
            {
                Input.Text += input + Environment.NewLine;
            }
            Result.IsEnabled = true;
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            //sublist is list of list of invidual line   
            task = 1;
            List<List<string>> sublists = new List<List<string>>();
            //it adds the string list to sublist and them start apppend the end of list with list string, if it reaches "0 0" and it 
            // is not the last line of the entire list, create a new list of list, and starting adding string to the later list.
            sublists.Add(new List<string>());
            int count = 0;
            foreach (var line in lines)
            {
                sublists.Last().Add(line);
                if (line.Contains("0 0") && count != (lines.Count() - 1))
                {
                    sublists.Add(new List<string>());
                }
                count++;
            }
            foreach (var sub in sublists)
            {
                Array.Clear(panel, 0, panel.GetLength(0) * panel.GetLength(1));
                Array.Clear(floyd, 0, floyd.GetLength(0) * floyd.GetLength(1));
                msg = "";
                int x;
                int y;
                visitable = 0;//v is the maximal visitable street
                // it creates panels one for dfs one for floyd, if two locations is not connected, it assigns a big number;
                for (int i = 1; i <= 21; i++)
                    for (int j = 1; j <= 21; j++)
                    {
                        panel[i, j] = maxn;
                        floyd[i, j] = maxn;
                    }
                firelocation = int.Parse(sub.First());
                string[] connected = sub.Skip(1).ToArray();
                ///if two streets are connected, mark with 1
                foreach (var conn in connected)
                {
                    string[] detail = conn.Split(' ');
                    x = int.Parse(detail[0]);
                    y = int.Parse(detail[1]);
                    panel[x, y] = 1;
                    panel[y, x] = 1;
                    floyd[x, y] = 1;
                    floyd[y, x] = 1;
                    //Then assign the the maximal connected street to the visitable
                    if (x > visitable)
                        visitable = x;
                    if (y > visitable)
                        visitable = y;
                }
                //floyd method to find shortest distance, it works with lots of matrixes
                for (int k = 1; k <= visitable; k++)
                    for (int i = 1; i <= visitable; i++)
                        for (int j = 1; j <= visitable; j++)
                            if (floyd[i, k] + floyd[k, j] < floyd[i, j])
                                floyd[i, j] = floyd[i, k] + floyd[k, j];
                //assign the start posotion, vis means visied location
                vis[1] = 1;
                availableRoute = 0;
                //first line, print case number 
                Output.Text += "Case " + task++ + Environment.NewLine;
                //dfs to search for routes
                dfs(1, 1);
                Output.Text += msg;
                Output.Text += $"There are {availableRoute} routes from the firestation to streetcorner {firelocation}" + Environment.NewLine;
            }
            Result.IsEnabled = false;

        }
    }
}

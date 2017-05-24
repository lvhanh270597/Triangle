using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using NCalc;

namespace TamGiac
{
    public partial class Form1 : Form
    {
        private Hashtable ways = new Hashtable();
        private string[] V = {"A", "B", "C", "a", "b", "c", "S", "ha", "hb", "hc"};
        private Hashtable E = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void setValueForVariable(string u, double v)
        {
            v = Math.Round(v, 3);
            if (u == "A")
            {
                A.ForeColor = Color.Green;
                v = (v * 180) / Math.PI;
                A.Text = v.ToString();
            }
            if (u == "B")
            {
                B.ForeColor = Color.Green;
                v = (v * 180) / Math.PI;
                B.Text = v.ToString();
            }
            if (u == "C")
            {
                C.ForeColor = Color.Green;
                v = (v * 180) / Math.PI;
                C.Text = v.ToString();
            }
            if (u == "a")
            {
                ca.ForeColor = Color.Green;
                ca.Text = v.ToString();
            }
            if (u == "b")
            {
                cb.ForeColor = Color.Green;
                cb.Text = v.ToString();
            }
            if (u == "c")
            {
                cc.ForeColor = Color.Green;
                cc.Text = v.ToString();
            }
            if (u == "S")
            {
                S.ForeColor = Color.Green;
                S.Text = v.ToString();
            }
            if (u == "ha")
            {
                ha.ForeColor = Color.Green;
                ha.Text = v.ToString();
            }
            if (u == "hb")
            {
                hb.ForeColor = Color.Green;
                hb.Text = v.ToString();
            }
            if (u == "hc")
            {
                hb.ForeColor = Color.Green;
                hc.Text = v.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            initVariable();
            while (true)
            {
                bool ok = false;
                bool exist = false;
                List<string> tmp1 = new List<string>();
                List<double> tmp2 = new List<double>();
                for (int p = 0; p < V.Length; p++)
                {
                    string u = V[p];
                    if (E[u] == null)
                    {
                        exist = true;
                        
                        // Danh sách tất cả các cách để tính u
                        List<ArrayList> ls = (List<ArrayList>)ways[u];
                        if (ls == null) continue;
                        // duyệt qua mỗi cách                        
                        foreach (ArrayList w in ls)
                        {                            
                            int n = Int32.Parse(w[0].ToString());                            
                            // chạy qua tất cả các biến cần để tính được u
                            int cnt = 0;
                            for (int i=1; i<=n; i++)
                            {
                                string v = w[i].ToString();
                                if (E[v] != null) cnt++;
                            }
                            if (cnt == n)
                            {
                                ok = true;
                                string des = w[n + 1].ToString();
                                Array a = des.Split();
                                string chuoiTinhToan = "";
                                for (int i=0; i<a.Length; i++)
                                {
                                    string str = a.GetValue(i).ToString();
                                    if (E[str] != null)
                                    {
                                        chuoiTinhToan += E[str].ToString();
                                    }
                                    else
                                        chuoiTinhToan += str;
                                }
                                Expression ee = new Expression(chuoiTinhToan);
                                tmp1.Add(u);
                                tmp2.Add((double)ee.Evaluate());
                                break;
                            }
                        }                 
                    }                   
                }
                if (!exist)
                {
                    MessageBox.Show("Solved all variable!");
                    return;
                }
                if (!ok)
                {
                    string st = "";
                    foreach (string str in V)
                        if (E[str] == null) st += str + " ";
                    MessageBox.Show("Can't solve " + st);
                    return;
                }
                for (int i = 0; i < tmp1.Count; i++)
                {
                    E[tmp1[i]] = tmp2[i];
                    setValueForVariable(tmp1[i], tmp2[i]);
                }
            }
        }
        private void analyse(string s)
        {            
            Array a = s.Split();            
            string u = a.GetValue(0).ToString();

            ArrayList al = new ArrayList();
            int n = Int32.Parse((string)a.GetValue(1));            
            for (int i = 0; i <= n; i++) al.Add(a.GetValue(i + 1));
            string des = "";
            for (int i = n + 2; i < a.Length; i++) des += a.GetValue(i) + " ";            
            al.Add(des);            
            if (ways[u] == null)
            {
                List<ArrayList> w = new List<ArrayList>();             
                w.Add(al);
                ways[u] = w;
            }
            else
            {
                List<ArrayList> w = (List<ArrayList>)ways[u];                
                w.Add(al);
                ways[u] = w;
            }
            
        }
        private void loadKnowLedge()
        {
            StreamReader str = new StreamReader(@"src\knowledge.txt");
            string line;
            while (true)
            {
                line = str.ReadLine();
                if (line == null) return;
                analyse(line);
            }
        }
        private void initVariable()
        {
            double d;
            if (A.Text.Length > 0)
            {
                d = Double.Parse(A.Text);
                d = (d * Math.PI) / 180;
                E["A"] = d;
            }
            if (B.Text.Length > 0)
            {
                d = Double.Parse(B.Text);
                d = (d * Math.PI) / 180;
                E["B"] = d;
            }
            if (C.Text.Length > 0)
            {
                d = Double.Parse(C.Text);
                d = (d * Math.PI) / 180;
                E["C"] = d;
            }
            if (ca.Text.Length > 0)
            {
                E["a"] = Double.Parse(ca.Text);
            }
            if (cb.Text.Length > 0)
            {
                E["b"] = Double.Parse(cb.Text);
            }
            if (cc.Text.Length > 0)
            {
                E["c"] = Double.Parse(cc.Text);
            }
            if (ha.Text.Length > 0)
            {
                E["ha"] = Double.Parse(ha.Text);
            }
            if (hb.Text.Length > 0)
            {
                E["hb"] = Double.Parse(hb.Text);
            }
            if (hc.Text.Length > 0)
            {
                E["hc"] = Double.Parse(hc.Text);
            }
            if (S.Text.Length > 0)
            {
                E["S"] = Double.Parse(S.Text);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // how many ways can solve a?
            // a 3 A B b sqrt(...)
            loadKnowLedge();
            /*List<ArrayList> ls = (List<ArrayList>)ways["b"];
            for (int i=0; i<ls.Count; i++)
            {
                string str = "";
                for (int j = 0; j < ls[i].Count; j++) str += ls[i][j].ToString();
                MessageBox.Show(str);
            }            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            E.Clear(); 
            A.Clear(); ca.Clear(); ha.Clear();
            B.Clear(); cb.Clear(); hb.Clear();
            C.Clear(); cc.Clear(); hc.Clear();
            S.Clear();            
        }
    }
}

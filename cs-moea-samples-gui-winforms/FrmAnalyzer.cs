using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MOEA.AlgorithmModels;
using MOEA.ComponentModels.SolutionModels;
using MOEA.ComponentModels;
using ZedGraph;
using MOEA.Benchmarks;

namespace MOEA.AnalyzerGUI
{
    using System.IO;
    using MOEA.Core.ProblemModels;
    using MOEA.Core.ComponentModels;
    public partial class FrmAnalyzer : Form
    {
        public enum AlgorithmType
        {
            NSGAII,
            HybridGame,
            HAPMOEA,
            GDE3
        }

        public enum ProblemType
        {
            NDND,
            NGPD,
            TNK,
            OKA2,
            SYMPART
        }

        public FrmAnalyzer()
        {
            InitializeComponent();
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;

            if (algorithm_type == AlgorithmType.NSGAII)
            {
                RunNSGAII<ContinuousVector>();
            }
            else if (algorithm_type == AlgorithmType.HybridGame)
            {
                RunHybridGame<ContinuousVector>();
            }
            else if (algorithm_type == AlgorithmType.HAPMOEA)
            {
                RunHAPMOEA<ContinuousVector>();
            }
            else if (algorithm_type == AlgorithmType.GDE3)
            {
                RunGDE3<ContinuousVector>();
            }
        }

        public IMOOProblem GetProblem()
        {
            ProblemType problem_type = (ProblemType)cboProblem.SelectedItem;
            if (problem_type == ProblemType.NDND)
            {
                return new NDNDProblem();
            }
            else if (problem_type == ProblemType.NGPD)
            {
                return new NGPDProblem();
            }
            else if (problem_type == ProblemType.TNK)
            {
                return new TNKProblem();
            }
            else if (problem_type == ProblemType.OKA2)
            {
                return new OKA2Problem();
            }
            else if (problem_type == ProblemType.SYMPART)
            {
                return new SYMPARTProblem();
            }
            return null;
        }

        private void ClearStatusInfo()
        {
            txtStatus1.Text = "";
            txtStatus2.Text = "";
            txtStatus2.Text = "";
        }

        public void RunGDE3<S>()
            where S : ContinuousVector, new()
        {
            GDE3<S> algorithm = new GDE3<S>(GetProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunHAPMOEA<S>()
            where S : ContinuousVector, new()
        {
            HAPMOEA<S> algorithm = new HAPMOEA<S>(GetProblem());

            algorithm.Config.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunHybridGame<S>()
            where S : ContinuousVector, new()
        {
            HybridGame<S> algorithm = new HybridGame<S>(GetProblem());

            algorithm.Config.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunNSGAII<S>()
            where S : ContinuousVector, new()
        {
            NSGAII<S> algorithm = new NSGAII<S>(GetProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
                {
                    while (!algorithm.IsTerminated)
                    {
                        algorithm.Evolve();
                        worker.ReportProgress(0);
                    }
                };
            worker.ProgressChanged += (s1, e1) =>
                {
                    txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                    //Console.WriteLine("Number of Dominating Improvements: {0}", algorithm.NumberOfDominatingImprovements);
                    txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
                };
            worker.RunWorkerCompleted += (s1, e1) =>
                {
                    NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                    DisplayParetoFront(archive);
                };
            worker.RunWorkerAsync();
        }

        private Color GetParetoFrontColor()
        {
            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;
            if (algorithm_type == AlgorithmType.HybridGame)
            {
                return Color.Blue;
            }
            else if (algorithm_type == AlgorithmType.NSGAII)
            {
                return Color.Green;
            }
            else if (algorithm_type == AlgorithmType.HAPMOEA)
            {
                return Color.Orange;
            }
            else if (algorithm_type == AlgorithmType.GDE3)
            {
                return Color.Pink;
            }
            return Color.Black;
        }

        private void DisplayParetoFront<S>(NondominatedPopulation<S> archive)
            where S : ContinuousVector, new()
        {
            GraphPane pane = chtParetoFront.GraphPane;
            pane.XAxis.Title.Text = "Objective 1";
            pane.YAxis.Title.Text = "Objective 2";
            pane.Title.Text = string.Format("Problem: {0}", (ProblemType)cboProblem.SelectedItem);

            PointPairList list = new PointPairList();

            foreach (S s in archive.Solutions)
            {
                list.Add(s.FindObjectiveAt(0), s.FindObjectiveAt(1));
            }

            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;
            LineItem myCurve = pane.AddCurve(string.Format("{0} ({1})", algorithm_type.ToString(), cboProblem.SelectedItem), list, GetParetoFrontColor());
            myCurve.Symbol.Type=ZedGraph.SymbolType.Plus;
            myCurve.Line.IsVisible = false;

            pane.AxisChange();
            chtParetoFront.Invalidate();

            DataTable table = new DataTable();
            table.Columns.Add("#");
            IMOOProblem problem=archive[0].Problem;
            for(int i=0; i < problem.GetObjectiveCount(); ++i)
            {
                table.Columns.Add(string.Format("Objective {0}", i + 1));
            }
            for (int i = 0; i < problem.GetDimensionCount(); ++i)
            {
                table.Columns.Add(string.Format("x[{0}]", i));
            }

            for (int i = 0; i < archive.Count; ++i)
            {
                S s = archive[i];
                List<object> values = new List<object>();
                values.Add(i + 1);
                for(int j=0; j < problem.GetObjectiveCount(); ++j)
                {
                    values.Add(s.FindObjectiveAt(j));
                }
                for (int j = 0; j < problem.GetDimensionCount(); ++j)
                {
                    values.Add(s[j]);
                }
                table.Rows.Add(values.ToArray());
            }

            dgvParetoFront.DataSource = table;
        }

        private void FrmMOEA_Load(object sender, EventArgs e)
        {
            cboAlgorithm.DataSource = Enum.GetValues(typeof(AlgorithmType));
            cboProblem.DataSource = Enum.GetValues(typeof(ProblemType));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GraphPane pane = chtParetoFront.GraphPane;
            pane.CurveList.Clear();
            chtParetoFront.Invalidate();
        }

        private void cboProblem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string problem_name = cboProblem.SelectedItem.ToString();
            string filename=string.Format("{0}.htm", problem_name);
            if (File.Exists(filename))
            {
                wbProblem.Navigate(string.Format("file://{0}", Path.GetFullPath(filename)));
            }
        }
    }
}

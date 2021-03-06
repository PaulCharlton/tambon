﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace De.AHoerstemeier.Tambon
{
    public partial class CreationStatisticForm : Form
    {
        public CreationStatisticForm()
        {
            InitializeComponent();
        }

        private void btnCalcTambon_Click(object sender, EventArgs e)
        {
            CreationStatisticsTambon statistics = new CreationStatisticsTambon((Int32)edtYearStart.Value, (Int32)edtYearEnd.Value);
            DoCalculate(statistics);
        }

        private void btnCalcMuban_Click(object sender, EventArgs e)
        {
            CreationStatisticsMuban statistics = new CreationStatisticsMuban((Int32)edtYearStart.Value, (Int32)edtYearEnd.Value);
            DoCalculate(statistics);
        }

        private void btnDates_Click(object sender, EventArgs e)
        {
            StatisticsAnnouncementDates statistics = new StatisticsAnnouncementDates((Int32)edtYearStart.Value, (Int32)edtYearEnd.Value);
            DoCalculate(statistics);
            if ( statistics.StrangeAnnouncements.Any() )
            {
                // Invoke(new Action(() => RoyalGazetteViewer.ShowGazetteDialog(statistics.StrangeAnnouncements, false)));
            }
        }

        private void DoCalculate(AnnouncementStatistics statistics)
        {
            statistics.Calculate(GlobalData.AllGazetteAnnouncements);
            edtData.Text = statistics.Information();
        }

        private void btnCalcAmphoe_Click(object sender, EventArgs e)
        {
            CreationStatisticsAmphoe statistics = new CreationStatisticsAmphoe((Int32)edtYearStart.Value, (Int32)edtYearEnd.Value);
            DoCalculate(statistics);
        }

        private void CreationStatisticForm_Load(object sender, EventArgs e)
        {
            edtYearEnd.Maximum = DateTime.Now.Year;
            edtYearEnd.Value = edtYearEnd.Maximum;
            edtYearStart.Maximum = edtYearEnd.Maximum;
        }
    }
}